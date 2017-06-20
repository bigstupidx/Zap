using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI
{
    public class UnlockableButton : WarpStoreButton
    {
        [SerializeField]
        private Banner m_PriceBannerPrefab;
        private Banner m_PriceBannerInstance;
        [SerializeField]
        private int m_Price;

        [SerializeField]
        private ParticleSystem m_TrailPSPrefab;
        [SerializeField]
        private RotatingTrail m_RotatingTrailObject;

        [SerializeField]
        private StatusIcon m_LockPrefab;
        private StatusIcon m_LockInstance;
        [SerializeField]
        private bool m_IsUnlocked = false;
        [SerializeField]
        private Color m_LockedColor;
        [SerializeField]
        private Color m_UnlockedColor;

        public delegate void UnlockButton_EventHandler(UnlockableButton button);
        public static event UnlockButton_EventHandler UnlockButtonEvent;

        private void Awake()
        {
            UnlockButtonEvent += this.handleUnlockButtonPress;
        }

        private void handleUnlockButtonPress(UnlockableButton button)
        {
            if(this != button)
            {
                if(m_IsUnlocked)
                {
                    m_LockInstance.SetUnlocked();
                }
            }
            else
            {
                GameMaster.Instance.m_PlayerDecorations.SetTrailPS(m_TrailPSPrefab);
                m_LockInstance.SetEquipped();
            }
        }

        public override void Init()
        {
            base.Init();

            // instantiate banner
            if (m_PriceBannerPrefab != null)
            {
                m_PriceBannerInstance = Instantiate(m_PriceBannerPrefab, this.transform);
                m_PriceBannerInstance.transform.localPosition = new Vector3(0, m_RectTransform.rect.height / 2.0f, 0);
                m_PriceBannerInstance.SetText(m_Price.ToString());
            }

            // instantiate particle system
            ParticleSystem ps = Instantiate(m_TrailPSPrefab, m_RotatingTrailObject.transform);
            ps.transform.localPosition = Vector3.zero;

            // instantiate lock prefab
            if(m_LockPrefab != null)
            {
                m_LockInstance = Instantiate(m_LockPrefab, this.transform) as StatusIcon;
                m_LockInstance.transform.localPosition = Vector3.zero;
                m_LockInstance.SetLocked();
                m_BackgroundImage.color = m_LockedColor;
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
                equip();
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
                m_BackgroundImage.color = m_UnlockedColor;
                m_LockInstance.SetUnlocked();
                Destroy(m_PriceBannerInstance.gameObject);
            }
        }

        private void equip()
        {
            // change which button has check mark on it
            UnlockButtonEvent(this);
        }
    }
}
