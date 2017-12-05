using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    [RequireComponent(typeof(Image))]
    public class UITextBlink : MonoBehaviour
    {
        private Text m_Text;

        [SerializeField]
        private float m_BlinkTime;
        private float m_NewBlinkTime;
        [SerializeField]
        private Color m_FadeOutColor = new Color(255, 255, 255, 0);
        private float m_CurrTime;
        private Color m_OriginalColor;

        private bool m_IsFadingIn;

        void Start()
        {
            m_Text = GetComponent<Text>();
            m_OriginalColor = m_Text.color;
            m_IsFadingIn = true;
            m_NewBlinkTime = m_BlinkTime;
        }

        void Update()
        {
            m_CurrTime += Time.deltaTime;
            if (m_IsFadingIn)
            {
                m_Text.color = Color.Lerp(m_FadeOutColor, m_OriginalColor, m_CurrTime / m_BlinkTime);
            }
            else
            {
                m_Text.color = Color.Lerp(m_OriginalColor, m_FadeOutColor, m_CurrTime / m_BlinkTime);
            }

            if (m_CurrTime >= m_BlinkTime)
            {
                m_CurrTime = 0.0f;
                m_IsFadingIn = !m_IsFadingIn;

                // if new blink time set then do this so we smoothly transition to new blink time
                if (m_BlinkTime != m_NewBlinkTime)
                {
                    m_BlinkTime = m_NewBlinkTime;
                }
            }
        }
    }
}