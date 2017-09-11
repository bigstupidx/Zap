using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class AtmosphereFire : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem m_Fire1;

        [SerializeField]
        private ParticleSystem m_Fire2;

        public void Play()
        {
            if (m_Fire1 != null && m_Fire2 != null)
            {
                m_Fire1.Play();
                m_Fire2.Play();
            }
        }

        public void Stop()
        {
            if (m_Fire1 != null && m_Fire2 != null)
            {
                m_Fire1.Stop();
                m_Fire2.Stop();
            }
        }
    }
}
