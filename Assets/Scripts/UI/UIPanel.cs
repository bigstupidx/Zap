using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class UIPanel : MonoBehaviour
    {
        [SerializeField]
        private bool m_HideOnStart;

        [SerializeField]
        private AudioClip m_SelectSound;

        void Awake()
        {
            if (m_HideOnStart)
            {
                Hide();
                m_HideOnStart = false; // this is so when we call gameobject.setactive it wont hide this again
            }
        }

        void Start()
        {
            //m_AudioSource = this.GetComponent<AudioSource>();
        }

        public virtual void Show() { this.gameObject.SetActive(true); }
        public virtual void Hide() { this.gameObject.SetActive(false); }

        public void PlaySelectSound()
        {
            AudioSource audio = AudioManager.Instance.Spawn2DAudio(m_SelectSound, true);
            if (audio != null && m_SelectSound != null)
            {
                audio.clip = m_SelectSound;
                audio.Play();
                Destroy(audio.gameObject, audio.clip.length);
            }
        }
    }
}
