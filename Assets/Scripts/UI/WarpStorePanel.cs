using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAP;
using UnityEngine.UI;

namespace UI
{
    public class WarpStorePanel : UIPanel
    {
        [Tooltip("Reference to Scroll Rect of the scroll view that displays content in warp store")]
        public ScrollRect m_ScrollRect;

        public WarpPanel m_MainMenuPanel;
        public WarpPanel m_CurrencyPanel;
        public WarpPanel m_BoostPanel;
        public WarpPanel m_TrailsPanel;

        private WarpPanel m_CurrPanel;

        public enum ActionType
        {
            BUY_50_ZAPS,
            BUY_100_ZAPS,
            BUY_250_ZAPS,
            BUY_1000_ZAPS,

            OPEN_MAIN_MENU,
            OPEN_CURRENCY_MENU,
            OPEN_TRAILS_MENU,
            OPEN_BOOSTS_MENU,
        }

        private void Start()
        {
            TriggerAction(ActionType.OPEN_MAIN_MENU);
        }

        public void TriggerAction(ActionType action)
        {
            switch(action)
            {
                case ActionType.BUY_50_ZAPS:
                    Buy50Zaps();
                    break;
                case ActionType.BUY_100_ZAPS:
                    Buy100Zaps();
                    break;
                case ActionType.BUY_250_ZAPS:
                    Buy250Zaps();
                    break;
                case ActionType.BUY_1000_ZAPS:
                    Buy1000Zaps();
                    break;
                case ActionType.OPEN_MAIN_MENU:
                    OpenMainMenuPanel();
                    break;
                case ActionType.OPEN_CURRENCY_MENU:
                    OpenCurrencyPanel();
                    break;
                case ActionType.OPEN_TRAILS_MENU:
                    OpenTrailsPanel();
                    break;
                case ActionType.OPEN_BOOSTS_MENU:
                    OpenBoostsPanel();
                    break;
            }
        }

        // IAP consumables
        private void Buy50Zaps()
        {
            IAPManager.Instance.Buy50Zaps();
        }
        private void Buy100Zaps()
        {
            IAPManager.Instance.Buy100Zaps();
        }
        private void Buy250Zaps()
        {
            IAPManager.Instance.Buy250Zaps();
        }
        private void Buy1000Zaps()
        {
            IAPManager.Instance.Buy1000Zaps();
        }

        // UI panel functions
        private void ShowPanel(WarpPanel warpPanel)
        {
            if (warpPanel == null)
            {
                Debug.LogWarning("WARNING: new warp panel is null");
                return;
            }

            if (m_CurrPanel != null)
            {
                m_CurrPanel.Hide();
            }

            m_CurrPanel = warpPanel;
            m_ScrollRect.content = warpPanel.GetRectTransform();
            warpPanel.Show();
        }
        private void OpenMainMenuPanel()
        {
            ShowPanel(m_MainMenuPanel);
        }
        private void OpenCurrencyPanel()
        {
            ShowPanel(m_CurrencyPanel);
        }
        private void OpenTrailsPanel()
        {
            ShowPanel(m_TrailsPanel);
        }
        private void OpenBoostsPanel()
        {
            ShowPanel(m_BoostPanel);
        }
        public void GoBack()
        {
            if (m_CurrPanel == null)
            {
                return;
            }

            TriggerAction(m_CurrPanel.GetPreviousPanelActionType());
        }
        public void GoHome()
        {
            TriggerAction(ActionType.OPEN_MAIN_MENU);
        }
    }
}
