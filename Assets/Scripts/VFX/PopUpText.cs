using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    [RequireComponent(typeof(TextMesh))]
    public class PopUpText : MonoBehaviour
    {
        [SerializeField]
        private float m_Lifetime;
        [SerializeField]
        private float m_TargetUniformScale;
        [SerializeField]
        private float m_LerpTime;
        [SerializeField]
        private bool m_ShouldFadeOut;

        private Vector3 m_InitialScale;
        private Vector3 m_TargetScale;
        private TextMesh m_Text;

        private bool m_IsGettingLarger;
        private float m_CurrLerpTime;
        private float m_CurrLifetime;
        private bool m_ShouldLerp;

        void Awake()
        {
            m_Text = GetComponent<TextMesh>();
        }

        void Start()
        {
            m_ShouldLerp = true;
            m_TargetScale = new Vector3(
                m_TargetUniformScale, 
                m_TargetUniformScale, 
                m_TargetUniformScale);
            m_CurrLerpTime = 0.0f;
            m_IsGettingLarger = true;
            m_InitialScale = this.transform.localScale;
            this.transform.localScale = Vector3.zero;
        }

        void Update()
        {
            countDownDeathTimer();
            if(m_ShouldLerp)
            {
                lerp();
            }
        }

        private void countDownDeathTimer()
        {
            m_CurrLifetime += Time.deltaTime;

            if(m_ShouldFadeOut)
            {
                m_Text.color = new Color(
                    m_Text.color.r,
                    m_Text.color.g,
                    m_Text.color.b,
                    Mathf.Lerp(1.0f, 0.0f, m_CurrLifetime));
            }

            if (m_CurrLifetime >= m_Lifetime)
            {
                Destroy(this.gameObject);
            }
        }

        private void lerp()
        {
            Vector3 newScale = Vector3.zero;
            m_CurrLerpTime += Time.deltaTime;
            float percentage = m_CurrLerpTime / m_LerpTime;
            if(m_IsGettingLarger)
            {
                newScale = Vector3.Lerp(
                    m_InitialScale, 
                    m_TargetScale,
                    percentage);
            }
            else
            {
                newScale = Vector3.Lerp(
                    m_TargetScale,
                    m_InitialScale,
                    percentage);
            }
            this.transform.localScale = newScale;

            if(percentage >= 1.0f)
            {
                if(!m_IsGettingLarger)
                {
                    m_ShouldLerp = false;
                }
                m_IsGettingLarger = !m_IsGettingLarger;
                m_CurrLerpTime = 0.0f;
            }
        }

        public void SetColor(Color col)
        {
            if (m_Text != null)
                m_Text.color = col;
        }

        public void SetText(string str)
        {
            if (m_Text != null)
                m_Text.text = str;
        }
    }
}
