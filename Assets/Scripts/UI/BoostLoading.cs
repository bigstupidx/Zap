using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class BoostLoading : MonoBehaviour
    {

        public Text m_PercentageText;

        [SerializeField]
        private float m_ChargeTime;
        private float m_CurrentCharge;

        private Image m_LoadImage;

        private void Awake()
        {
            m_CurrentCharge = 0;
            m_LoadImage = GetComponent<Image>();
        }

        void Start()
        {
            StartCoroutine(Load());
        }

        public IEnumerator Load()
        {
            while (m_CurrentCharge < m_ChargeTime)
            {
                m_CurrentCharge += Time.deltaTime;
                if (m_PercentageText != null && m_LoadImage != null)
                {
                    float percentage = m_CurrentCharge / m_ChargeTime;
                    m_LoadImage.fillAmount = percentage;
                    m_PercentageText.text = Mathf.FloorToInt(percentage * 100.0f) + "%";
                }
                yield return null;
            }
        }
    }
}