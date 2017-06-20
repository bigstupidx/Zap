using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WarpStoreButton : MonoBehaviour {

        [SerializeField]
        [Tooltip("Main image to display")]
        private Sprite m_Main;
        [SerializeField]
        [Tooltip("Reference to image container that will change its image")]
        private Image m_MainImage;

        [SerializeField]
        [Tooltip("Background to display")]
        private Sprite m_Background;
        protected Image m_BackgroundImage;

        [SerializeField]
        [Tooltip("Text to display describing button")]
        protected string m_Text;
        [SerializeField]
        [Tooltip("Reference to banner that contains text over button")]
        private Banner m_Banner;

        [Tooltip("Action type that defines what will happen when button is pressed")]
        public WarpStorePanel.ActionType m_ActionType;

        protected RectTransform m_RectTransform;

        void Awake()
        {
            m_RectTransform = this.GetComponent<RectTransform>();
        }

        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            // set background image
            m_BackgroundImage = GetComponent<Image>();
            if (m_BackgroundImage != null)
            {
                if (m_Background != null)
                {
                    m_BackgroundImage.sprite = m_Background;
                }
            }

            // set main image
            if (m_MainImage != null)
            {
                if(m_Main != null)
                {
                    m_MainImage.sprite = m_Main;
                    m_MainImage.rectTransform.sizeDelta =
                        new Vector2(m_BackgroundImage.rectTransform.sizeDelta.x * 1.0f,
                        m_BackgroundImage.rectTransform.sizeDelta.y * 1.0f);
                }
            }

            // instantiate banner
            if(m_Banner != null)
            {
                m_Banner = Instantiate(m_Banner,this.transform);
                if(m_RectTransform == null)
                {
                    m_RectTransform = this.GetComponent<RectTransform>();
                }
                m_Banner.transform.localPosition = new Vector3(0, -m_RectTransform.rect.height / 2.0f, 0);
                m_Banner.SetText(m_Text);
            }

            // bind onclick() listener
            Button button = this.GetComponent<Button>();
            if(button != null)
            {
                button.onClick.AddListener(() => {
                    onButtonClick();
                });
            }
        }

        public virtual void onButtonClick()
        {
            WarpStorePanel warpStorePanel = GameCritical.GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel;
            if(warpStorePanel != null)
            {
                warpStorePanel.TriggerAction(m_ActionType);
            }
        }
    }
}