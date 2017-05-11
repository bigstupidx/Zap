using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class ZapScorer : MonoBehaviour
    {
        [SerializeField]
        private Color m_PositiveColor;
        [SerializeField]
        private Color m_NegativeColor;

        [SerializeField]
        private string m_ScoreString;

        [SerializeField]
        private List<List<Zap>> m_ZapComboList;

        private Text m_Text;

        // Use this for initialization
        void Start()
        {
            m_Text = GetComponent<Text>();
        }

        public void UpdateScore(int totalScore)
        {
            m_Text.text = m_ScoreString + totalScore;
        }
    }
}
