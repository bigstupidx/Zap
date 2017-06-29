using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class LoginPanelAnimationHandler : MonoBehaviour
    {

        [SerializeField]
        private Animator m_Animator;

        // Use this for initialization
        void Awake()
        {
            if (m_Animator == null)
            {
                m_Animator = this.GetComponent<Animator>();
            }
        }

        public void LoginSlideIn()
        {
            if (m_Animator != null)
            {
                m_Animator.SetBool("Login", true);
            }
        }

        public void SignUpSlideIn()
        {
            if (m_Animator != null)
            {
                m_Animator.SetBool("Login", false);
            }
        }
    }
}