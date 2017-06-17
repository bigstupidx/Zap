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
        [Range(0,1.0f)]
        private float m_PercentOfParent = 1.0f;

        [SerializeField]
        [Tooltip("Background to display")]
        private Sprite m_Background;
        private Image m_BackgroundImage;

        [SerializeField]
        [Tooltip("Text to display describing button")]
        private string m_Text;
        [SerializeField]
        [Tooltip("Reference to banner that contains text over button")]
        private Banner m_Banner;
        [SerializeField]
        [Tooltip("Where the banner should spawn")]
        private Transform m_BannerSpawn;

        [SerializeField]
        [Tooltip("Action type that defines what will happen when button is pressed")]
        private WarpStorePanel.ActionType m_ActionType;

        private void Start()
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
                        new Vector2(m_BackgroundImage.rectTransform.sizeDelta.x * m_PercentOfParent,
                        m_BackgroundImage.rectTransform.sizeDelta.y * m_PercentOfParent);
                }
            }

            // instantiate banner
            if(m_Banner != null)
            {
                m_Banner = Instantiate(
                    m_Banner, 
                    m_BannerSpawn.position, 
                    m_BannerSpawn.rotation, 
                    this.transform);
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

        private void onButtonClick()
        {
            WarpStorePanel warpStorePanel = GameCritical.GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel;
            if(warpStorePanel != null)
            {
                warpStorePanel.TriggerAction(m_ActionType);
            }
        }
    }
}