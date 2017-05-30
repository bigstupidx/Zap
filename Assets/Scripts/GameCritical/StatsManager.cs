using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UI;

namespace GameCritical
{
    public class StatsManager : MonoBehaviour
    {
        private int m_Score;
        public int Score { get { return m_Score; } }

        private bool m_FlawlessGridRun; // if this is true that means the player didnt hit anything bad in the current grid.

        // Use this for initialization
        void Start()
        {
            m_Score = 0;
            m_FlawlessGridRun = true;
        }

        public bool GetFlawlessGridRun()
        {
            return m_FlawlessGridRun;
        }

        public void SetFlawlessGridRun(bool isFlawless)
        {
            m_FlawlessGridRun = isFlawless;
        }

        public void AddToScore(int scoreToAdd)
        {
            m_Score += scoreToAdd;
            ZapScorer zapScorer = GameMaster.Instance.m_UIManager.m_InfoPanel.m_ZapScorer;
            if(zapScorer)
            {
                zapScorer.UpdateScore(m_Score);
                // If player hit a bad zap then disable flawless grid run bonus.
                if (scoreToAdd < 0)
                {
                    SetFlawlessGridRun(false);
                    zapScorer.PlayScoreAnimation(false);
                }
                else
                {
                    zapScorer.PlayScoreAnimation(true);
                }
            }
        }
    }
}
