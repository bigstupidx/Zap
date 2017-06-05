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
        private LoginPanelAnimationHandler m_LoginPanelAnimationHandler;

        void Awake()
        {
            if (m_LoginPanelAnimationHandler == null)
            {
                m_LoginPanelAnimationHandler = FindObjectOfType<LoginPanelAnimationHandler>();
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
            Debug.Log("Login button clicked");
            Login login = GameMaster.Instance.m_DatabaseManager.m_Login;
            if (login != null)
            {
                StartCoroutine(login.LoginUser(m_UsernameInputField.text, m_PasswordInputField.text));
            }
            else
            {
                Debug.LogError("Login script can't be found by LoginPanel");
            }
            /*
            Debug.Log("Login button clicked");
            Login login = GameMaster.Instance.m_DatabaseManager.m_Login;
            if(login != null)
            {
                // wait for login response
                yield return StartCoroutine(login.LoginUser(m_UsernameInputField.text, m_PasswordInputField.text));
            }
            else
            {
                Debug.LogError("Login script can't be found by LoginPanel");
            }
            */
        }
    }
}