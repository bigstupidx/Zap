using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Banner : MonoBehaviour
    {
        public Text m_Text;

        void Awake()
        {
            if(m_Text == null)
            {
                m_Text = GetComponentInChildren<Text>();
            }
        }

        public void SetText(string text)
        {
            if(m_Text)
            {
                m_Text.text = text;
            }
        }
    }
}
