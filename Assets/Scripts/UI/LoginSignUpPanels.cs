using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class LoginSignUpPanels : UIPanel
    {
        [SerializeField]
        private GameObject _signUpMenu;
        [SerializeField]
        private GameObject _loginMenu;

        [SerializeField]
        private Transform _SlideInLocation;
        [SerializeField]
        private Transform _SlideOutLocation;

        [SerializeField]
        private float _lerpTime;

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
            StartCoroutine(SlideTo(_SlideInLocation.position, _loginMenu));
            StartCoroutine(SlideTo(_SlideOutLocation.position, _signUpMenu));
        }

        public void SignUpSlideIn()
        {
            StartCoroutine(SlideTo(_SlideInLocation.position, _signUpMenu));
            StartCoroutine(SlideTo(_SlideOutLocation.position, _loginMenu));
        }
    }
}