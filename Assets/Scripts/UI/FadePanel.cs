using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FadePanel : UIPanel
    {
        [SerializeField]
        private float m_TransitionSpeed;
        [SerializeField]
        private Color m_FadeOutColor;
        [SerializeField]
        private Color m_FadeInColor;
        private Image m_Image;

        void Awake()
        {
            m_Image = GetComponent<Image>();
        }

        public void EndGameFadeOut()
        {
            StartCoroutine(fadeOut());
        }

        public void StartGameFadeIn()
        {
            StartCoroutine(fadeIn());
        }

        private IEnumerator fadeOut()
        {
            yield return StartCoroutine(transitionColors(m_Image.color, m_FadeOutColor));
            GameCritical.GameMaster.Instance.ReloadGameScene();
        }

        private IEnumerator fadeIn()
        {
            yield return StartCoroutine(transitionColors(m_FadeOutColor, m_FadeInColor));
        }

        private IEnumerator transitionColors(Color startColor, Color targetColor)
        {
            float lerpPercentage = 0.0f;
            while(lerpPercentage < 1.0f)
            {
                lerpPercentage += m_TransitionSpeed * Time.deltaTime;
                m_Image.color = Color.Lerp(startColor, targetColor, lerpPercentage);
                yield return null;
            }
        }
    }
}
