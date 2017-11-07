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

        private void Start()
        {
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_PARAM_USERNAME))
            {
                Hide();
                string username = SaveManager.GetString(Database.DatabaseConstants.m_PARAM_USERNAME);
                GameMaster.Instance.m_UIManager.m_InfoPanel.SetUsername(username);
            }
        }

        public override void Show()
        {
            base.Show();
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