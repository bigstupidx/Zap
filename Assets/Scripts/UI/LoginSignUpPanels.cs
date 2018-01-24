using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace UI
{
    public class LoginSignUpPanels : UIPanel
    {
        public SignUpPanel _signUpMenu;
        public LoginPanel _loginMenu;

        [SerializeField]
        private Transform _SlideInLocation;
        [SerializeField]
        private Transform _SlideOutLocation;

        [SerializeField]
        private float _lerpTime;

        private void Awake()
        {
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_PARAM_EMAIL))
            {
                string email = SaveManager.GetString(Database.DatabaseConstants.m_PARAM_EMAIL);
                string pass = SaveManager.GetString(Database.DatabaseConstants.m_PARAM_PASSWORD);
                GameMaster.Instance.m_UIManager.m_InfoPanel.SetEmail(email);
                GameMaster.Instance.m_UIManager.m_InfoPanel.SetPassword(pass);
                GameMaster.Instance.m_UIManager.m_LoginSignupPanels._loginMenu.m_EmailInputField.text = email;
                GameMaster.Instance.m_UIManager.m_LoginSignupPanels._loginMenu.m_PasswordInputField.text = pass;
                //GameMaster.Instance.m_UIManager.m_MainMenuPanel.m_LoginButton.gameObject.SetActive(false);
                //GameMaster.Instance.m_UIManager.m_MainMenuPanel.m_LogoutButton.gameObject.SetActive(true);
                GameMaster.Instance.m_UIManager.m_LoginSignupPanels._loginMenu.login();
            }
        }

        public void PlayOffline()
        {
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemoveUsername();
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemovePassword();
        }

        public override void Show()
        {
            base.Show();
            this.gameObject.SetActive(true);
            LoginSlideIn();
        }

        private IEnumerator SlideTo(Vector3 position, GameObject obj)
        {
            float currTime = 0.0f;
            Vector3 startPos = obj.transform.position;
            while(currTime < _lerpTime)
            {
                currTime += Time.deltaTime;
                obj.transform.position = Vector3.Lerp(startPos, position, currTime / _lerpTime);
                yield return null;
            }
        }

        public void LoginSlideIn()
        {
            StartCoroutine(SlideTo(_SlideInLocation.position, _loginMenu.gameObject));
            StartCoroutine(SlideTo(_SlideOutLocation.position, _signUpMenu.gameObject));
        }

        public void SignUpSlideIn()
        {
            StartCoroutine(SlideTo(_SlideInLocation.position, _signUpMenu.gameObject));
            StartCoroutine(SlideTo(_SlideOutLocation.position, _loginMenu.gameObject));
        }
    }
}