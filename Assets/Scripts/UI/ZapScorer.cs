using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(Animation))]
    public class ZapScorer : MonoBehaviour
    {
        [SerializeField]
        private string m_ScoreString;

        [SerializeField]
        private List<List<Zap>> m_ZapComboList;

        [SerializeField]
        private AnimationClip m_PositiveAnimation;

        [SerializeField]
        private AnimationClip m_NegativeAnimation;

        private Text m_Text;
        private Animation m_Animation;

        void Awake()
        {
            m_Text = GetComponent<Text>();
            m_Animation = GetComponent<Animation>();
        }

        public void PlayScoreAnimation(bool isPositiveChange)
        {
            if (m_Animation)
            {
                if(isPositiveChange)
                {
                    m_Animation.clip = m_PositiveAnimation;
                }
                else
                {
                    m_Animation.clip = m_NegativeAnimation;
                }

                m_Animation.Play();
            }
        }

        public void UpdateScoreString(int totalScore)
        {
            m_Text.text = m_ScoreString + totalScore;
            GameMaster.Instance.m_UIManager.m_ScorePanel.SetScoreText(totalScore);
        }
    }
}
