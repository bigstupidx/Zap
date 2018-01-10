using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using VFX;

namespace GameCritical
{
    public class UIManager : MonoBehaviour
    {
        public InfoPanel m_InfoPanel;
        public MainMenuPanel m_MainMenuPanel;
        public FadePanel m_FadePanel;
        public ShopCanvas m_ShopCanvas;
        public LevelPanel m_LevelPanel;
        public BoostLoading m_BoostLoading;
        public LoginSignUpPanels m_LoginSignupPanels;
        public ConfirmLogoutPanel m_ConfirmLogoutPanel;
        public ScoreMenuPanel m_ScorePanel;

        [SerializeField]
        private PopUpText m_PopUpTextPrefab;
        [SerializeField]
        private Vector3 m_PopUpTextOffset;

        [SerializeField]
        private NotificationPanel m_NotificationPanelPrefab;
        [SerializeField]
        private RectTransform m_NotificationSpawn;

        [SerializeField]
        private AudioClip m_DefaultButtonClickSound;

        void Awake()
        {
            if(m_ConfirmLogoutPanel == null)
            {
                m_ConfirmLogoutPanel = FindObjectOfType<ConfirmLogoutPanel>();
            }
            if (m_LoginSignupPanels == null)
            {
                m_LoginSignupPanels = FindObjectOfType<LoginSignUpPanels>();
            }
            if (m_InfoPanel == null)
            {
                m_InfoPanel = FindObjectOfType<InfoPanel>();
            }
            if (m_MainMenuPanel == null)
            {
                m_MainMenuPanel = FindObjectOfType<MainMenuPanel>();
            }
            if (m_FadePanel == null)
            {
                m_FadePanel = FindObjectOfType<FadePanel>();
            }
            if (m_LevelPanel == null)
            {
                m_LevelPanel = FindObjectOfType<LevelPanel>();
            }
            if (m_ShopCanvas == null)
            {
                m_ShopCanvas = FindObjectOfType<ShopCanvas>();
            }
            if (m_BoostLoading == null)
            {
                m_BoostLoading = FindObjectOfType<BoostLoading>();
            }
            if (m_ScorePanel == null)
            {
                m_ScorePanel = FindObjectOfType<ScoreMenuPanel>();
            }
        }

        public void SpawnUINotification(string message, bool isGoodNotification)
        {
            NotificationPanel notificationInstance = (NotificationPanel)Instantiate(m_NotificationPanelPrefab,
                m_NotificationSpawn.position,
                Quaternion.identity,
                this.transform);
            notificationInstance.SetText(message, isGoodNotification);
        }

        public PopUpText SpawnPopUpText(string str, Vector3 position, Color color)
        {
            PopUpText popUpTextPrefab = (PopUpText)Instantiate(m_PopUpTextPrefab,
                position + m_PopUpTextOffset,
                Quaternion.identity);
            popUpTextPrefab.SetText(str);
            popUpTextPrefab.SetColor(color);
            return popUpTextPrefab;
        }
    }
}
