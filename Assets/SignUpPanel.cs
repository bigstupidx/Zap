using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;
using Database;
using GameCritical;

namespace UI
{
    public class SignUpPanel : MonoBehaviour
    {
        [SerializeField]
        private Button m_SignUpButton;

        [SerializeField]
        private Button m_BackButton;

        [SerializeField]
        private InputField m_UsernameInputField;

        [SerializeField]
        private InputField m_PasswordInputField;

        [SerializeField]
        private InputField m_EmailInputField;

        [SerializeField]
        private Text m_ErrorMessage;

        [SerializeField]
        private LoginSignUpPanels m_LoginPanelAnimationHandler;

        [SerializeField]
        private Animation m_SignupSuccessfulAnimation;

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
            m_SignUpButton.onClick.AddListener(signup);
            m_BackButton.onClick.AddListener(back);
        }

        private void OnEnable()
        {
            m_UsernameInputField.text = "";
            m_PasswordInputField.text = "";
            m_EmailInputField.text = "";
        }

        private void back()
        {
            m_LoginPanelAnimationHandler.LoginSlideIn();
        }


        private void signupSuccess()
        {
            GameMaster.Instance.m_UIManager.m_LoginSignupPanels._loginMenu.m_EmailInputField.text = m_EmailInputField.text;
            //GameMaster.Instance.m_UIManager.m_LoginSignupPanels._loginMenu.m_PasswordInputField.text = m_PasswordInputField.text;
            m_ErrorMessage.text = "";
            m_UsernameInputField.text = "";
            m_PasswordInputField.text = "";
            m_EmailInputField.text = "";
            GameMaster.Instance.m_UIManager.m_LoginSignupPanels.LoginSlideIn();
            m_SignupSuccessfulAnimation.Play();
        }
        private void signupFailed()
        {
            m_ErrorMessage.text = "sign-up failed";
        }
        private void signupEmailAlreadyExists()
        {
            m_ErrorMessage.text = "email is already in use";
        }
        private void signup()
        {
            // Validate inputs
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(m_EmailInputField.text);
            if (m_UsernameInputField.text.Length <= 0 || m_PasswordInputField.text.Length <= 0 || m_EmailInputField.text.Length <= 0)
            {
                m_ErrorMessage.text = "please fill out all fields";
                return;
            }
            else if(!match.Success)
            {
                m_ErrorMessage.text = "please provide a valid email";
                return;
            }
            else
            {
                m_ErrorMessage.text = "";
            }

            // Create new user
            DataInserter dataInserter = GameMaster.Instance.m_DatabaseManager.m_DataInserter;
            if (dataInserter != null)
            {
                dataInserter.StartCoroutine(dataInserter.CreateUser(
                    m_UsernameInputField.text,
                    m_PasswordInputField.text,
                    m_EmailInputField.text,
                    signupSuccess,
                    signupFailed,
                    signupEmailAlreadyExists
                    ));
            }
            else
            {
                Debug.LogError("DataInserter script can't be found by SignUpPanel");
            }
        }
    }
}
