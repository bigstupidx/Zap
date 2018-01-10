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

        [SerializeField]
        private bool _costInZaps = false;
        public bool costInZaps
        {
            get
            {
                return _costInZaps;
            }
            set
            {
                _costInZaps = value;
                if(_costInZaps == true)
                {
                    m_PriceBannerInstance.m_Text.color = Constants.Instance.ZapCurrencyTextColor;
                }
                else
                {
                    m_PriceBannerInstance.m_Text.color = Constants.Instance.ScoreTextColor;
                }
            }
        }

        [SerializeField]
        private string m_Description;

        protected bool m_IsUnlocked = false;

        private Banner m_PriceBannerInstance;
        protected StatusIcon m_StatusInstance;

        public delegate void UnlockButton_EventHandler(UnlockableButton button);
        public static event UnlockButton_EventHandler UnlockButtonEvent;

        private static AudioClip cashRegisterSound;
        private static AudioClip equipSound;

        private void Awake()
        {
            if(cashRegisterSound == null)
            {
                cashRegisterSound = Resources.Load<AudioClip>(PrefabFinder.AUDIO + "Sounds/Cash register");
            }
            if (equipSound == null)
            {
                equipSound = Resources.Load<AudioClip>(PrefabFinder.AUDIO + "Sounds/EquipSound");
            }
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
                    int totalAvailableCurrency = 0;
                    if (_costInZaps)
                    {
                        totalAvailableCurrency = statManager.GetZaps();
                    }
                    else
                    {
                        totalAvailableCurrency = statManager.GetScore();
                    }

                    if (totalAvailableCurrency >= m_Price)
                    {
                        string descriptionText = m_Text;
                        if(m_Description != "")
                        {
                            descriptionText = m_Description;
                        }
                        specialUICanvas.m_ConfirmPurchasePanel.Populate(successfulPurchaseCallback, descriptionText.ToUpper());
                    }
                    else
                    {
                        if(_costInZaps)
                        {
                            specialUICanvas.m_FailedPurchasePanelForZaps.Show();
                        }
                        else
                        {
                            specialUICanvas.m_FailedPurchasePanelForPoints.Show();
                        }
                    }
                }
            }
        }

        private void successfulPurchaseCallback()
        {
            StatsManager statManager = GameMaster.Instance.m_StatsManager;
            if (statManager)
            {
                if (_costInZaps)
                {
                    statManager.AddZaps(-m_Price);
                }
                else
                {
                    statManager.AddToScore(-m_Price);
                }
                m_IsUnlocked = true;
                m_BackgroundImage.color = Constants.Instance.UnlockedColor;
                m_StatusInstance.SetUnlocked();
                Destroy(m_PriceBannerInstance.gameObject);
                // play purchase sound
                AudioManager.Instance.Spawn2DAudio(cashRegisterSound, true);

                handleUnlockButtonPress(this);
            }
        }

        public virtual void equip()
        {
            // play equip sound
            AudioManager.Instance.Spawn2DAudio(equipSound,true);
        }
    }
}
