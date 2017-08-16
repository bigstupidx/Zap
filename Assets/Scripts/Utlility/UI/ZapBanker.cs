using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class ZapBanker : MonoBehaviour
    {
        [SerializeField]
        private string m_PreString;

        [SerializeField]
        private AnimationClip m_PositiveAnimation;

        [SerializeField]
        private Image m_ZapCurrencyImage;

        [SerializeField]
        private AnimationClip m_NegativeAnimation;
        private Animation m_Animation;

        private Text m_Text;

        void Awake()
        {
            m_Text = GetComponent<Text>();
            m_Animation = GetComponent<Animation>();
        }

        public Vector3 GetImagePosition()
        {
            return m_ZapCurrencyImage.transform.position;
        }

        private void PlayZapScoreAnimation(bool isPositiveChange)
        {
            if (m_Animation)
            {
                if (isPositiveChange)
                {
                    m_Animation.clip = m_PositiveAnimation;
                }
                else
                {
                    m_Animation.clip = m_NegativeAnimation;
                }

                m_Animation.Play();
            }
        }

        public void UpdateZapsString(int zaps)
        {
            m_Text.text = m_PreString + zaps;
            if(zaps > 0)
            {
                PlayZapScoreAnimation(true);
            }
            else
            {
                PlayZapScoreAnimation(false);
            }
        }
    }
}
