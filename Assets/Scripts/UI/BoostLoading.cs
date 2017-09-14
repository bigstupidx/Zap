using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class BoostLoading : MonoBehaviour
    {
        public Text m_PercentageText;

        public Vector3 m_InGamePosition;
        public Vector3 m_WarpStorePosition;

        [SerializeField]
        private Image m_LoadImage;

        [SerializeField]
        private Image m_BoostImage;

        [SerializeField]
        private float m_SlideLerpTime = 1.0f;

        private Button m_Button;

        private void Awake()
        {
            HideBoostImage();
            m_PercentageText.text = "NO BOOST";
            m_Button = GetComponent<Button>();
        }

        private void Start()
        {
            m_Button.onClick.AddListener(() => GameMaster.Instance.m_PlayerBoost.StartCoroutine("ActivateCurrentBooster"));
        }

        public void SlideToInGamePosition()
        {
            StartCoroutine(slideToPosition(m_InGamePosition));
        }

        public void SlideToWarpStorePosition()
        {
            StartCoroutine(slideToPosition(m_WarpStorePosition));
        }

        private IEnumerator slideToPosition(Vector3 targetPos)
        {
            RectTransform rect = this.GetComponent<RectTransform>();
            float currLerpTime = 0;
            while (currLerpTime <= m_SlideLerpTime)
            {
                currLerpTime += Time.deltaTime;
                rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPos, currLerpTime / m_SlideLerpTime);
                yield return null;
            }
        }

        public void SetPercentage(float percentage)
        {
            if (m_PercentageText != null && m_LoadImage != null)
            {
                m_LoadImage.fillAmount = percentage;
                m_BoostImage.gameObject.SetActive(false);
                m_PercentageText.text = Mathf.FloorToInt(percentage * 100.0f) + "%";
            }

            if(percentage >= 100.0f)
            {
                ShowBoostImage();
            }
        }

        public void HideBoostImage()
        {
            m_BoostImage.gameObject.SetActive(false);
        }

        public void ShowBoostImage()
        {
            m_BoostImage.gameObject.SetActive(true);
            m_PercentageText.text = "";
        }

        public void SetText(string text)
        {
            m_PercentageText.text = text;
        }

        public void SetBoostSprite(Sprite sprite)
        {
            m_BoostImage.sprite = sprite;
            ShowBoostImage();
        }
    }
}