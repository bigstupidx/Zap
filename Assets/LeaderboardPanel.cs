using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardPanel : UIPanel
    {
        [SerializeField]
        private VerticalLayoutGroup m_VerticalLayoutGroup;
        [SerializeField]
        private LeaderboardPlayerProfile m_LeaderBoardProfilePrefab;
        [SerializeField]
        private GameObject m_LoadingIcon;

        protected override void Awake()
        {
            base.Awake();
            if (m_VerticalLayoutGroup == null)
            {
                m_VerticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
            }
            DepopulateLeaderboard();
            ShowLoadingAnimation();
        }

        public override void Show()
        {
            base.Show();
            ShowLoadingAnimation();
            DepopulateLeaderboard();
            //PopulateLeaderboard();
        }

        private void DepopulateLeaderboard()
        {
            int numChildren = m_VerticalLayoutGroup.transform.childCount;
            for(int i = 0; i < numChildren; i++)
            {
                Destroy(m_VerticalLayoutGroup.transform.GetChild(i).gameObject);
            }
        }

        private void ShowLoadingAnimation()
        {
            m_LoadingIcon.gameObject.SetActive(true);
        }

        public void PopulateLeaderboard()
        {
            m_LoadingIcon.gameObject.SetActive(false);
            for (int i = 0; i < 20; i++)
            {
                LeaderboardPlayerProfile entry = Instantiate(m_LeaderBoardProfilePrefab, m_VerticalLayoutGroup.transform);
            }
        }
    }
}
