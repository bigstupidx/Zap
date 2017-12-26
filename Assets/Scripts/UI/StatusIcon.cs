using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class StatusIcon : MonoBehaviour
    {

        [SerializeField]
        private Sprite m_LockCenterSprite;
        [SerializeField]
        private Sprite m_LockTopRightSprite;
        [SerializeField]
        private Sprite m_CheckmarkSprite;
        [SerializeField]
        private Sprite m_TransparentSprite;

        private Image m_Image;

        void Awake()
        {
            m_Image = GetComponent<Image>();
            m_Image.sprite = m_LockTopRightSprite;
        }

        public void SetUnlocked()
        {
            if(m_Image)
                m_Image.sprite = m_TransparentSprite;
        }

        public void SetCenterLocked()
        {
            if (m_Image)
                m_Image.sprite = m_LockCenterSprite;
        }

        public void SetTopRightLocked()
        {
            if (m_Image)
                m_Image.sprite = m_LockTopRightSprite;
        }

        public void SetEquipped()
        {
            if (m_Image)
                m_Image.sprite = m_CheckmarkSprite;
        }
    }
}