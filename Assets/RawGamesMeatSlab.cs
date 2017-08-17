using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RawGamesMeatSlab : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_MeatSlapSound;
        [SerializeField]
        private AudioSource m_AudioSource1;

        [SerializeField]
        private AudioClip m_MeatSpinningSound;
        [SerializeField]
        private AudioSource m_AudioSource2;

        void Awake()
        {
            if(m_AudioSource1 != null && m_AudioSource2 != null)
            {
                m_AudioSource1.clip = m_MeatSlapSound;
                m_AudioSource2.clip = m_MeatSpinningSound;
            }
            else
            {
                Debug.LogError("No audio sources defined in RawGamesMeatSlab");
            }
        }

        public void PlayMeatSpinSound()
        {
            if (m_MeatSpinningSound != null)
            {
                m_AudioSource2.Play();
            }
        }

        public void PlayMeatSlapSound()
        {
            if(m_MeatSlapSound != null)
            {
                m_AudioSource1.Play();
            }
        }
    }
}
