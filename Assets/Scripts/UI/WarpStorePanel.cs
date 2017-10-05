using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAP;
using UnityEngine.UI;
using GameCritical;
using Player;

namespace UI
{
    public class WarpStorePanel : UIPanel
    {
        [Tooltip("Reference to Scroll Rect of the scroll view that displays content in warp store")]
        public ScrollRect m_ScrollRect;

        [SerializeField]
        private float m_SlideLerpTime;

        public Vector3 m_OffScreenPosition;
        public Vector3 m_OnScreenPosition;

        public SpecialUICanvas m_SpecialUICanvas;

        public WarpPanel m_MainMenuPanel;
        public WarpPanel m_CurrencyPanel;
        public WarpPanel m_BoostPanel;
        public WarpPanel m_CustomizePanel;
        public TrailsPanel m_TrailsPanel;
        public WarpPanel m_ShapesPanel;
        public WarpPanel m_PatternsPanel;

        private WarpPanel m_CurrPanel;

        public enum CurrencyActionType
        {
            BUY_50_ZAPS,
            BUY_100_ZAPS,
            BUY_250_ZAPS,
            BUY_1000_ZAPS,
        }

        public enum NavActionType
        {
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
            ShowPanel(m_MainMenuPanel);
        }

        public override void Hide()
        {
            base.Hide();
            RectTransform rect = this.GetComponent<RectTransform>();
            rect.anchoredPosition = m_OffScreenPosition;
        }

        private IEnumerator slideTo(Vector3 targetPos)
        {
            RectTransform rect = this.GetComponent<RectTransform>();
            float currLerpTime = 0;
            while(currLerpTime <= m_SlideLerpTime)
            {
                currLerpTime += Time.deltaTime;
                rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPos, currLerpTime / m_SlideLerpTime);
                yield return null;
            }
        }

        public IEnumerator SlideOut()
        {
            GameMaster.Instance.m_UIManager.m_BoostLoading.SlideToInGamePosition();
            yield return StartCoroutine(slideTo(m_OffScreenPosition));
            GameMaster.Instance.m_WarpZoneManager.SetInputEnabled(false);
            Hide();
        }

        public IEnumerator SlideIn()
        {
            GameMaster.Instance.m_UIManager.m_BoostLoading.SlideToWarpStorePosition();
            yield return StartCoroutine(slideTo(m_OnScreenPosition));
            GameMaster.Instance.m_WarpZoneManager.SetInputEnabled(true);
        }

        public override void Show()
        {
            if(!this.gameObject.activeInHierarchy)
            {
                base.Show();
                //play slide in animation
                StartCoroutine("SlideIn");
            }

            // disalbe player from activating equipped ability
            PlayerBoost playerBooster = GameMaster.Instance.m_PlayerBoost;
            playerBooster.Reset(); /* call this first to reset so that if the booster is cooling down 
            it wont re-allow a player to press on it while in the warpstore to activate it.*/
            playerBooster.canActivate = false; // then don't let player activate the booster in warp store.
        }

        public void TriggerCurrencyAction(CurrencyActionType action)
        {
            switch (action)
            {
                case CurrencyActionType.BUY_50_ZAPS:
                    Buy50Zaps();
                    break;
                case CurrencyActionType.BUY_100_ZAPS:
                    Buy100Zaps();
                    break;
                case CurrencyActionType.BUY_250_ZAPS:
                    Buy250Zaps();
                    break;
                case CurrencyActionType.BUY_1000_ZAPS:
                    Buy1000Zaps();
                    break;
            }
        }

        public void TriggerNavAction(NavActionType action)
        {
            switch(action)
            {
                case NavActionType.OPEN_MAIN_MENU:
                    OpenMainMenuPanel();
                    break;
                case NavActionType.OPEN_CURRENCY_MENU:
                    OpenCurrencyPanel();
                    break;
                case NavActionType.OPEN_BOOSTS_MENU:
                    OpenBoostsPanel();
                    break;
                case NavActionType.OPEN_CUSTOMIZE_MENU:
                    OpenCustomizePanel();
                    break;
                case NavActionType.OPEN_TRAILS_MENU:
                    OpenTrailsPanel();
                    break;
                case NavActionType.OPEN_SHAPES_MENU:
                    OpenShapesPanel();
                    break;
                case NavActionType.OPEN_PATTERNS_MENU:
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
        private void ShowPanel(WarpPanel warpPanelPrefab)
        {
            if (warpPanelPrefab == null)
            {
                Debug.LogWarning("WARNING: new warp panel is null");
                return;
            }

            if (m_CurrPanel != null)
            {
                m_CurrPanel.Hide();
                Destroy(m_CurrPanel.gameObject);
            }

            WarpPanel warpPanelInstance = Instantiate(warpPanelPrefab, m_ScrollRect.transform);
            warpPanelInstance.transform.localPosition = Vector3.zero;
            m_CurrPanel = warpPanelInstance;
            m_ScrollRect.content = warpPanelInstance.GetRectTransform();
            warpPanelInstance.Show();
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

            TriggerNavAction(m_CurrPanel.GetPreviousPanelActionType());
        }
        public void GoHome()
        {
            TriggerNavAction(NavActionType.OPEN_MAIN_MENU);
        }
    }
}
