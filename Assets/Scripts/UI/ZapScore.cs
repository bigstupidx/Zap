using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class ZapScore : MonoBehaviour
    {
        [SerializeField]
        private Color m_PositiveColor;
        [SerializeField]
        private Color m_NegativeColor;

        [SerializeField]
        private string m_ScoreString;

        private Text m_Text;
        private int m_Score;

        // Use this for initialization
        void Start()
        {
            m_Text = GetComponent<Text>();
        }

        public void AddToScore(int scoreToAdd)
        {
            m_Score += scoreToAdd;
            m_Text.text = m_ScoreString + m_Score;
        }

        public int GetScore()
        {
            return m_Score;
        }
    }
}
