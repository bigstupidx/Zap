using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class AtmosphereFire : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem m_Fire1;

        public void Play()
        {
            if (m_Fire1 != null)
            {
                m_Fire1.Play();
            }
        }

        public void Stop()
        {
            if (m_Fire1 != null)
            {
                m_Fire1.Stop();
            }
        }
    }
}
