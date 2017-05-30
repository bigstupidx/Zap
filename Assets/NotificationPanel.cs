using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

[RequireComponent(typeof(DestroyTimer))]
[RequireComponent(typeof(AudioSource))]
public class NotificationPanel : MonoBehaviour {

    [SerializeField]
    private AudioClip m_GoodNotificationSound;
    [SerializeField]
    private AudioClip m_BadNotificationSound;
    private AudioSource m_AudioSource;

    [SerializeField]
    private Text m_Text;

    // Use this for initialization
    void Start () {
        if (m_Text == null)
        {
            m_Text = GetComponentInChildren<Text>();
        }
        if(m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
	}

    private void PlayNotificationSound(bool isGoodNotification)
    {
        if(m_AudioSource)
        {
            if(m_GoodNotificationSound && m_BadNotificationSound)
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
        if(m_Text)
        {
            m_Text.text = text;
        }

        PlayNotificationSound(isGoodNotification);
    }
}
