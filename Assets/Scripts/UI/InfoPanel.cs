using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class InfoPanel : MonoBehaviour {

        public ZapScorer m_ZapScorer;
        public ZapBanker m_ZapBanker;
        public Text m_DeathStartMultiplierText;
        public Text m_Username;
        public string m_Password;
        public Text m_StageText;
        public string m_DeathStarMultiplierStr;

        void Start()
        {
            m_Password = "";
            if(m_ZapScorer == null)
            {
                m_ZapScorer = FindObjectOfType<ZapScorer>();
            }
            if (m_ZapBanker == null)
            {
                m_ZapBanker = FindObjectOfType<ZapBanker>();
            }


            StatsManager statsManager = GameMaster.Instance.m_StatsManager;
            if (statsManager != null)
            {
                if (m_ZapBanker != null)
                {
                    m_ZapBanker.UpdateZapsString(statsManager.GetZaps());
                }
                else
                {
                    Debug.LogError("Zap Banker in InfoPanel is null");
                }
            }
        }

        public void SetStage(int stage)
        {
            m_StageText.text = "STAGE: " + stage.ToString();
            GameMaster.Instance.m_UIManager.m_ScorePanel.SetStageText(stage);
        }

        public void SetEmail(string email)
        {
            m_Username.text = email;
            SaveManager.SetString(Database.DatabaseConstants.m_PARAM_EMAIL, email);
        }

        public void SetPassword(string pass)
        {
            m_Password = pass;
            SaveManager.SetString(Database.DatabaseConstants.m_PARAM_PASSWORD, pass);
        }

        public void RemovePassword()
        {
            m_Password = "";
            SaveManager.RemoveKey(Database.DatabaseConstants.m_PARAM_PASSWORD);
        }

        public void RemoveUsername()
        {
            m_Username.text = "";
            SaveManager.RemoveKey(Database.DatabaseConstants.m_PARAM_EMAIL);
        }

        public void SetDeathStarMultiplierText(float multiplier)
        {
            if(m_DeathStartMultiplierText != null)
            {
                m_DeathStartMultiplierText.text = m_DeathStarMultiplierStr + multiplier;
            }
        }
    }
}
