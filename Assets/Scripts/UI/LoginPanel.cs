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
        [SerializeField]
        private InputField m_UsernameInputField;

        [SerializeField]
        private InputField m_PasswordInputField;

        [SerializeField]
        private Button m_LoginButton;

        [SerializeField]
        private Button m_SignupButton;

        [SerializeField]
        private Text m_ErrorMessage;

        [SerializeField]
        private LoginSignUpPanels m_LoginPanelAnimationHandler;

        void Awake()
        {
            m_ErrorMessage.text = "";
            if (m_LoginPanelAnimationHandler == null)
            {
                m_LoginPanelAnimationHandler = FindObjectOfType<LoginSignUpPanels>();
            }
        }

        // Use this for initialization
        void Start()
        {
            m_SignupButton.onClick.AddListener(signup);
            m_LoginButton.onClick.AddListener(login);
        }

        private void signup()
        {
            Debug.Log("Signup button clicked");
            if (m_LoginPanelAnimationHandler != null)
            {
                m_LoginPanelAnimationHandler.SignUpSlideIn();
            }
        }

        private void login()
        {
            if(m_UsernameInputField.text.Length <= 0 || m_PasswordInputField.text.Length <= 0)
            {
                m_ErrorMessage.text = "please type a user and password";
            }
            else
            {
                m_ErrorMessage.text = "";
            }

            Debug.Log("Login request sent to database");
            /*Login login = GameMaster.Instance.m_DatabaseManager.m_Login;
            if (login != null)
            {
                StartCoroutine(login.LoginUser(m_UsernameInputField.text, m_PasswordInputField.text));
            }
            else
            {
                Debug.LogError("Login script can't be found by LoginPanel");
            }*/
        }
    }
}