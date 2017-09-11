using System.Collections;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Blink : MonoBehaviour
    {

        private SpriteRenderer m_SpriteRenderer;

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
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_OriginalColor = m_SpriteRenderer.color;
            m_IsFadingIn = true;
            m_NewBlinkTime = m_BlinkTime;
        }

        public IEnumerator ChangeBlinkSpeed(Blink exclamationBlinker, float blinkSpeedChangeInterval)
        {
            yield return new WaitForSeconds(blinkSpeedChangeInterval);
            exclamationBlinker.MultiplyBlinkTime(0.5f);
            StartCoroutine(ChangeBlinkSpeed(exclamationBlinker, blinkSpeedChangeInterval / 2.0f));
        }

        public void MultiplyBlinkTime(float multiplier)
        {
            if (multiplier > 0)
            {
                m_NewBlinkTime *= multiplier;
            }
        }

        void Update()
        {
            m_CurrTime += Time.deltaTime;
            if (m_IsFadingIn)
            {
                m_SpriteRenderer.color = Color.Lerp(m_FadeOutColor, m_OriginalColor, m_CurrTime / m_BlinkTime);
            }
            else
            {
                m_SpriteRenderer.color = Color.Lerp(m_OriginalColor, m_FadeOutColor, m_CurrTime / m_BlinkTime);
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