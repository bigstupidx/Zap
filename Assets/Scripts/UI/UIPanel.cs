using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class UIPanel : MonoBehaviour
    {
        [SerializeField]
        private bool m_HideOnStart;

        [SerializeField]
        private AudioClip m_SelectSound;

        private AudioSource m_AudioSource;

        // Use this for initialization
        void Start()
        {
            if(m_HideOnStart)
            {
                Hide();
            }

            m_AudioSource = this.GetComponent<AudioSource>();
        }

        public virtual void Show() { this.gameObject.SetActive(true); }
        public virtual void Hide() { this.gameObject.SetActive(false); }

        public void PlaySelectSound()
        {
            if (m_AudioSource != null && m_SelectSound != null)
            {
                m_AudioSource.clip = m_SelectSound;
                m_AudioSource.Play();
            }
        }
    }
}
