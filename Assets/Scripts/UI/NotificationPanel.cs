using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    [RequireComponent(typeof(DestroyTimer))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Image))]
    public class NotificationPanel : MonoBehaviour
    {

        [SerializeField]
        private AudioClip m_GoodNotificationSound;
        [SerializeField]
        private AudioClip m_BadNotificationSound;
        private AudioSource m_AudioSource;

        [SerializeField]
        private Color _goodNotifColor;

        [SerializeField]
        private Color _badNotifColor;

        [SerializeField]
        private Text m_Text;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        // Use this for initialization
        void Start()
        {
            if (m_Text == null)
            {
                m_Text = GetComponentInChildren<Text>();
            }
            if (m_AudioSource == null)
            {
                m_AudioSource = GetComponent<AudioSource>();
            }
        }

        private void PlayNotificationSound(bool isGoodNotification)
        {
            if (m_AudioSource)
            {
                if (m_GoodNotificationSound && m_BadNotificationSound)
                {
                    m_AudioSource.clip = (isGoodNotification) ? m_GoodNotificationSound : m_BadNotificationSound;
                    m_AudioSource.Play();
                }
                else
                {
                    Debug.LogWarning("Notification sounds are null");
                }
            }
        }

        public void SetText(string text, bool isGoodNotification)
        {
            if (m_Text)
            {
                m_Text.text = text;
            }

            // set the notifcation color based on the type of notifcation it is
            if(isGoodNotification)
            {
                _image.color = _goodNotifColor;
            }
            else
            {
                _image.color = _badNotifColor;
            }

            PlayNotificationSound(isGoodNotification);
        }
    }
}