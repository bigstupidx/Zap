using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    [RequireComponent(typeof(Animation))]
    public class LockdownCircle : MonoBehaviour
    {
        private Animation m_Animation;

        void Awake()
        {
            m_Animation = GetComponent<Animation>();
        }

        void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void PlayLockdown()
        {
            this.gameObject.SetActive(true);
            m_Animation.Play();
        }

        public void StopLockdown()
        {
            this.gameObject.SetActive(false);
        }
    }
}
