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
        public PopUpText m_PopUpTextPrefab;
        [SerializeField]
        private Vector3 m_PopUpTextOffset;
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

        public void AddToScore(int scoreToAdd, Vector3 position)
        {
            m_Score += scoreToAdd;
            m_Text.text = m_ScoreString + m_Score;
            //Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);
            PopUpText popUpTextPrefab = (PopUpText)Instantiate(m_PopUpTextPrefab, position + m_PopUpTextOffset, Quaternion.identity);
            popUpTextPrefab.SetText(scoreToAdd.ToString());
            if(scoreToAdd >= 0)
            {
                popUpTextPrefab.SetColor(m_PositiveColor);
            }
            else
            {
                popUpTextPrefab.SetColor(m_NegativeColor);
            }
        }

        public int GetScore()
        {
            return m_Score;
        }
    }
}
