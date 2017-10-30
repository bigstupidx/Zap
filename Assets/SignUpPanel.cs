using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;

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

        private void back()
        {
            m_LoginPanelAnimationHandler.LoginSlideIn();
        }

        private void signup()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(m_EmailInputField.text);

            if (m_UsernameInputField.text.Length <= 0 || m_PasswordInputField.text.Length <= 0 || m_EmailInputField.text.Length <= 0)
            {
                m_ErrorMessage.text = "please fill out all fields";
            }
            else if(!match.Success)
            {
                m_ErrorMessage.text = "please provide a valid email";
            }
            else
            {
                m_ErrorMessage.text = "";
            }

            Debug.Log("Signup request sent to database");
        }
    }
}
