using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoPanel : MonoBehaviour {

        public ZapScorer m_ZapScorer;
        public Text m_DeathStartMultiplierText;
        public string m_DeathStarMultiplierStr;

        void Start() {
            if(m_ZapScorer == null)
            {
                m_ZapScorer = FindObjectOfType<ZapScorer>();
            }
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
