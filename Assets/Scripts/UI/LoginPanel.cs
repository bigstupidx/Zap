using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;
using Database;

namespace UI
{
    public class LoginPanel : MonoBehaviour
    {
        public InputField m_EmailInputField;
        
        public InputField m_PasswordInputField;

        [SerializeField]
        private Button m_LoginButton;

        [SerializeField]
        private Button m_SignupButton;

        [SerializeField]
        private Text m_ErrorMessage;

        [SerializeField]
        private LoginSignUpPanels m_LoginPanelAnimationHandler;

        [SerializeField]
        private Animation m_LoginSuccessfulAnimation;

        [SerializeField]
        private Animation m_LoginFailedAnimation;

        [SerializeField]
        private Image m_LoadingImage;

        void Awake()
        {
            m_ErrorMessage.text = "";
            if (m_LoginPanelAnimationHandler == null)
            {
                m_LoginPanelAnimationHandler = FindObjectOfType<LoginSignUpPanels>();
            }
            m_PasswordInputField.shouldHideMobileInput = true;
        }

        // Use this for initialization
        void Start()
        {
            m_SignupButton.onClick.AddListener(signup);
            m_LoginButton.onClick.AddListener(login);
        }

        public void ShowLoadingImage()
        {
            m_LoadingImage.gameObject.SetActive(true);
        }

        public void HideLoadingImage()
        {
            m_LoadingImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            m_EmailInputField.text = "";
            m_PasswordInputField.text = "";
        }


        private void signup()
        {
            Debug.Log("Signup button clicked");
            if (m_LoginPanelAnimationHandler != null)
            {
                m_LoginPanelAnimationHandler.SignUpSlideIn();
            }
        }

        #region Login Methods
        private void loginSuccess()
        {
            GameMaster.Instance.m_UIManager.m_InfoPanel.SetEmail(m_EmailInputField.text);
            GameMaster.Instance.m_UIManager.m_InfoPanel.SetPassword(m_PasswordInputField.text);
            GameMaster.Instance.m_UIManager.m_LoginSignupPanels.gameObject.SetActive(false);
            GameMaster.Instance.m_UIManager.m_MainMenuPanel.m_LogoutButton.gameObject.SetActive(true);
            GameMaster.Instance.m_UIManager.m_MainMenuPanel.m_LoginButton.gameObject.SetActive(false);
            m_ErrorMessage.text = "";
            m_LoginSuccessfulAnimation.Play();
            HideLoadingImage();
            // send current highscore to database if we are logged in
            GameMaster.Instance.m_StatsManager.SendLocalHighscoreToDatabase();
        }
        private void loginFailed()
        {
            m_LoginFailedAnimation.Play();
            m_ErrorMessage.text = "login failed";
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemovePassword();
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemoveUsername();
            HideLoadingImage();
        }
        public void login()
        {
            if(m_EmailInputField.text.Length <= 0 || m_PasswordInputField.text.Length <= 0)
            {
                m_ErrorMessage.text = "please type an email and password";
                return;
            }
            else
            {
                m_ErrorMessage.text = "";
            }

            ShowLoadingImage();

            DataLoader dataLoader = GameMaster.Instance.m_DatabaseManager.m_DataLoader;
            if(dataLoader != null)
            {

                dataLoader.StartCoroutine(dataLoader.AuthenticateUser(
                    m_EmailInputField.text, 
                    m_PasswordInputField.text,
                    loginSuccess,
                    loginFailed
                    ));
            }
            else
            {
                Debug.LogError("DataLoader script can't be found by LoginPanel");
            }

        }
        #endregion
    }
}