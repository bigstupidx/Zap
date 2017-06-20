using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAP;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class WarpStorePanel : UIPanel
    {
        [Tooltip("Reference to Scroll Rect of the scroll view that displays content in warp store")]
        public ScrollRect m_ScrollRect;

        public SpecialUICanvas m_SpecialUICanvas;

        public WarpPanel m_MainMenuPanel;
        public WarpPanel m_CurrencyPanel;
        public WarpPanel m_BoostPanel;
        public WarpPanel m_CustomizePanel;
        public TrailsPanel m_TrailsPanel;
        public WarpPanel m_ShapesPanel;
        public WarpPanel m_PatternsPanel;

        private WarpPanel m_CurrPanel;

        public enum ActionType
        {
            BUY_50_ZAPS,
            BUY_100_ZAPS,
            BUY_250_ZAPS,
            BUY_1000_ZAPS,

            BUY_FROST_TRAIL,
            BUY_HEATREAT_TRAIL,
            BUY_PERIWINKLE_TRAIL,
            BUY_ZED_TRAIL,

            OPEN_MAIN_MENU,
            OPEN_CURRENCY_MENU,
            OPEN_BOOSTS_MENU,
            OPEN_CUSTOMIZE_MENU,

            OPEN_TRAILS_MENU,
            OPEN_SHAPES_MENU,
            OPEN_PATTERNS_MENU,
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
                case ActionType.BUY_FROST_TRAIL:
                    break;
                case ActionType.BUY_HEATREAT_TRAIL:
                    break;
                case ActionType.BUY_PERIWINKLE_TRAIL:
                    break;
                case ActionType.BUY_ZED_TRAIL:
                    break;
                case ActionType.OPEN_MAIN_MENU:
                    OpenMainMenuPanel();
                    break;
                case ActionType.OPEN_CURRENCY_MENU:
                    OpenCurrencyPanel();
                    break;
                case ActionType.OPEN_BOOSTS_MENU:
                    OpenBoostsPanel();
                    break;
                case ActionType.OPEN_CUSTOMIZE_MENU:
                    OpenCustomizePanel();
                    break;
                case ActionType.OPEN_TRAILS_MENU:
                    OpenTrailsPanel();
                    break;
                case ActionType.OPEN_SHAPES_MENU:
                    OpenShapesPanel();
                    break;
                case ActionType.OPEN_PATTERNS_MENU:
                    OpenPatternsPanel();
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
        private void OpenCustomizePanel()
        {
            ShowPanel(m_CustomizePanel);
        }
        private void OpenBoostsPanel()
        {
            ShowPanel(m_BoostPanel);
        }
        private void OpenTrailsPanel()
        {
            ShowPanel(m_TrailsPanel);
        }
        private void OpenShapesPanel()
        {
            ShowPanel(m_ShapesPanel);
        }
        private void OpenPatternsPanel()
        {
            ShowPanel(m_PatternsPanel);
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
