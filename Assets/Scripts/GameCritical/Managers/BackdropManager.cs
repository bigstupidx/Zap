using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miscellaneous;

namespace GameCritical
{
    public class BackdropManager : MonoBehaviour
    {

        [SerializeField]
        private Backdrop m_Backdrop;

        [SerializeField]
        private List<Color> m_StartColors;
        [SerializeField]
        private List<Color> m_EndColors;
        private int m_CurrColorIndex = -1;

        [SerializeField]
        private Color m_WarpStoreColor1;
        [SerializeField]
        private Color m_WarpStoreColor2;

        [SerializeField]
        private float m_LerpTime;
        private float m_LerpAmount;

        private Color m_PrevColor1;
        private Color m_PrevColor2;
        private Color m_NextColor1;
        private Color m_NextColor2;
        private bool m_IsLerping;

        void Awake()
        {
            m_CurrColorIndex = 0;
        }

        void Start()
        {
            m_LerpAmount = 0.0f;
            m_IsLerping = false;
            if (m_Backdrop == null)
            {
                m_Backdrop = FindObjectOfType<Backdrop>();
            }
        }

        void Update()
        {
            if (m_IsLerping)
            {
                m_LerpAmount += Time.deltaTime;
                float m_LerpPercentage = m_LerpAmount / m_LerpTime;
                Color currColor1 = Color.Lerp(m_PrevColor1, m_NextColor1, m_LerpPercentage);
                Color currColor2 = Color.Lerp(m_PrevColor2, m_NextColor2, m_LerpPercentage);
                SetColors(currColor1, currColor2);

                if (m_LerpPercentage >= 1.0f)
                {
                    m_IsLerping = false;
                    m_LerpAmount = 0.0f;
                }
            }
        }

        private void ChangeColors(Color col1, Color col2)
        {
            m_LerpAmount = 0.0f;
            m_IsLerping = true;
            m_PrevColor1 = m_Backdrop.GetFirstColor();
            m_PrevColor2 = m_Backdrop.GetSecondColor();
            m_NextColor1 = col1;
            m_NextColor2 = col2;
        }

        public void ShowNextStageColors()
        {
            if(m_CurrColorIndex + 1 <= m_StartColors.Count && m_CurrColorIndex + 1 <= m_EndColors.Count)
            {
                m_CurrColorIndex++;
                Color startColor = m_StartColors[m_CurrColorIndex];
                Color endColor = m_EndColors[m_CurrColorIndex];
                ChangeColors(startColor, endColor);
            }
        }

        public void ShowWarpStoreColors()
        {
            ChangeColors(m_WarpStoreColor1, m_WarpStoreColor2);
        }

        public void SetColors(Color col1, Color col2)
        {
            m_Backdrop.SetFirstColor(col1);
            m_Backdrop.SetSecondColor(col2);
        }
    }
}
