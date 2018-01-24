using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UI;

namespace GameCritical
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField]
        private bool debugScore;
        private int m_Score = 0;
        private int m_NumZaps = 1200;
        private int m_Highscore = 0;

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

            // create heartbeat for obtaining the highscore from database
            InvokeRepeating("GetScoreHeartbeat", 0.0f, 4.0f);
        }
        
        void Start()
        {
            m_Score = 0;
            m_FlawlessGridRun = true;
            m_ZapScorer.UpdateScoreString(m_Score);
            m_ZapBanker.UpdateZapsString(m_NumZaps);
            if (debugScore)
            {
                AddToScore(30000);
            }
        }

        public void SendLocalHighscoreToDatabase()
        {
            StartCoroutine(LocalHighscoreToDatabase());
        }

        private IEnumerator LocalHighscoreToDatabase()
        {
            // attempt to set the highscore in the database in case the local score is better
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_PARAM_EMAIL) &&
                SaveManager.IsStringStored(Database.DatabaseConstants.m_HIGHSCORE))
            {
                yield return StartCoroutine(GameMaster.Instance.m_DatabaseManager.m_DataInserter.SetHighScore(
                    SaveManager.GetString(Database.DatabaseConstants.m_PARAM_EMAIL),
                    SaveManager.GetInt(Database.DatabaseConstants.m_HIGHSCORE),
                    null,
                    null
                    ));
            }
        }

        public int GetHighscore()
        {
            return m_Highscore;
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

        private void GetScoreHeartbeat()
        {
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_PARAM_EMAIL))
            {
                StartCoroutine(GameMaster.Instance.m_DatabaseManager.m_DataLoader.GetHighscore(
                    SaveManager.GetString(Database.DatabaseConstants.m_PARAM_EMAIL), null, null));
            }
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
