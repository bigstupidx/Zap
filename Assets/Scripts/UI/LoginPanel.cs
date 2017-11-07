﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCritical;
using Database;

namespace UI
{
    public class LoginPanel : MonoBehaviour
    {
        public InputField m_UsernameInputField;
        
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

        private void OnEnable()
        {
            m_UsernameInputField.text = "";
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
            GameMaster.Instance.m_UIManager.m_InfoPanel.SetUsername(m_UsernameInputField.text);
            GameMaster.Instance.m_UIManager.m_LoginSignupPanels.gameObject.SetActive(false);
            m_ErrorMessage.text = "";
            m_LoginSuccessfulAnimation.Play();
        }
        private void loginFailed()
        {
            m_ErrorMessage.text = "login failed";
        }
        private void login()
        {
            if(m_UsernameInputField.text.Length <= 0 || m_PasswordInputField.text.Length <= 0)
            {
                m_ErrorMessage.text = "please type a user and password";
                return;
            }
            else
            {
                m_ErrorMessage.text = "";
            }

            DataLoader dataLoader = GameMaster.Instance.m_DatabaseManager.m_DataLoader;
            if(dataLoader != null)
            {
                dataLoader.StartCoroutine(dataLoader.AuthenticateUser(
                    m_UsernameInputField.text, 
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