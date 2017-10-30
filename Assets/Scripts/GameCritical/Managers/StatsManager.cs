using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UI;

namespace GameCritical
{
    public class StatsManager : MonoBehaviour
    {
        private int m_Score = 0;
        private int m_NumZaps = 1200;

        private bool m_FlawlessGridRun; // if this is true that means the player didnt hit anything bad in the current grid.

        private ZapScorer m_ZapScorer;
        private ZapBanker m_ZapBanker;

        void Awake()
        {
            InfoPanel infoPanel = GameMaster.Instance.m_UIManager.m_InfoPanel;
            if(infoPanel == null)
            {
                infoPanel = FindObjectOfType<InfoPanel>();
            }

            m_ZapBanker = infoPanel.m_ZapBanker;
            m_ZapScorer = infoPanel.m_ZapScorer;
            if (m_ZapScorer == null)
            {
                m_ZapScorer = FindObjectOfType<ZapScorer>();
            }
            if(m_ZapBanker == null)
            {
                m_ZapBanker = FindObjectOfType<ZapBanker>();
            }
        }
        
        void Start()
        {
            m_Score = 0;
            m_FlawlessGridRun = true;
            m_ZapScorer.UpdateScoreString(m_Score);
            m_ZapBanker.UpdateZapsString(m_NumZaps);
        }

        public bool GetFlawlessGridRun()
        {
            return m_FlawlessGridRun;
        }

        public void SetFlawlessGridRun(bool isFlawless)
        {
            m_FlawlessGridRun = isFlawless;
        }

        public int GetZaps()
        {
            return m_NumZaps;
        }

        public void AddZaps(int zapsToAdd)
        {
            m_NumZaps += zapsToAdd;
            if (m_ZapBanker != null)
            {
                m_ZapBanker.UpdateZapsString(m_NumZaps);
            }
        }

        public int GetScore()
        {
            return m_Score;
        }

        public void AddToScore(int scoreToAdd)
        {
            m_Score += scoreToAdd;
            if(m_ZapScorer != null)
            {
                m_ZapScorer.UpdateScoreString(m_Score);
                // If player hit a bad zap then disable flawless grid run bonus.
                if (scoreToAdd < 0)
                {
                    SetFlawlessGridRun(false);
                    m_ZapScorer.PlayScoreAnimation(false);
                }
                else
                {
                    m_ZapScorer.PlayScoreAnimation(true);
                }
            }
        }
    }
}
