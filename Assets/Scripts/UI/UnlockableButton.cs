using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UnityEngine.UI;
using UnityEngine.Events;
using Utility;

namespace UI
{
    public class UnlockableButton : WarpStoreButton
    {
        [SerializeField]
        private int m_Price;

        protected bool m_IsUnlocked = false;

        private Banner m_PriceBannerInstance;
        protected StatusIcon m_StatusInstance;

        public delegate void UnlockButton_EventHandler(UnlockableButton button);
        public static event UnlockButton_EventHandler UnlockButtonEvent;

        private void Awake()
        {
            UnlockButtonEvent += this.handleUnlockButtonPress;
        }

        public void handleUnlockButtonPress(UnlockableButton button)
        {
            if (this != button)
            {
                if (m_IsUnlocked)
                {
                    m_StatusInstance.SetUnlocked();
                }
            }
            else
            {
                equip();
                m_StatusInstance.SetEquipped();
            }
        }

        public override void Init()
        {
            base.Init();

            // instantiate banner
            if (Constants.Instance.PriceBannerPrefab != null)
            {
                m_PriceBannerInstance = Instantiate(Constants.Instance.PriceBannerPrefab, this.transform);
                m_PriceBannerInstance.transform.localPosition = new Vector3(0, m_RectTransform.rect.height / 2.0f, 0);
                m_PriceBannerInstance.SetText(m_Price.ToString());
            }

            // instantiate lock prefab
            if(Constants.Instance.StatusPrefab != null)
            {
                m_StatusInstance = Instantiate(Constants.Instance.StatusPrefab, this.transform) as StatusIcon;
                m_StatusInstance.transform.localPosition = Vector3.zero;
                m_StatusInstance.SetTopRightLocked();
                m_BackgroundImage.color = Constants.Instance.LockedColor;
            }
            else
            {
                Debug.LogWarning("No lock icon set on unlockable button");
            }
        }

        public override void onButtonClick()
        {
            if(!m_IsUnlocked)
            {
                buy();
            }
            else
            {
                // change which button has check mark on it and fire off equip event
                UnlockButtonEvent(this);
            }
        }

        private void buy()
        {
            Debug.Log("buy unlockable");
            StatsManager statManager = GameMaster.Instance.m_StatsManager;
            if (statManager != null)
            {
                SpecialUICanvas specialUICanvas = GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel.m_SpecialUICanvas;
                if(specialUICanvas != null)
                {
                    if (statManager.GetZaps() >= m_Price)
                    {
                        specialUICanvas.m_ConfirmPurchasePanel.Populate(successfulPurchaseCallback, m_Text);
                    }
                    else
                    {
                        specialUICanvas.m_FailedPurchasePanel.Show();
                    }
                }
            }
        }

        private void successfulPurchaseCallback()
        {
            StatsManager statManager = GameMaster.Instance.m_StatsManager;
            if (statManager)
            {
                statManager.AddZaps(-m_Price);
                m_IsUnlocked = true;
                m_BackgroundImage.color = Constants.Instance.UnlockedColor;
                m_StatusInstance.SetUnlocked();
                Destroy(m_PriceBannerInstance.gameObject);
            }
        }

        public virtual void equip()
        {

        }
    }
}
