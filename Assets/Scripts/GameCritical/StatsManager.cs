using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace GameCritical
{
    public class StatsManager : MonoBehaviour
    {

        private int m_Score;
        public int Score { get { return m_Score; } }

        // Use this for initialization
        void Start()
        {
            m_Score = 0;
        }

        public void AddToScore(int scoreToAdd)
        {
            m_Score += scoreToAdd;
            GameMaster.Instance.m_UIManager.m_InfoPanel.m_ZapScorer.UpdateScore(m_Score);
        }
    }
}
