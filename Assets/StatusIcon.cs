using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StatusIcon : MonoBehaviour {

    [SerializeField]
    private Sprite m_LockCenterSprite;
    [SerializeField]
    private Sprite m_LockTopRightSprite;
    [SerializeField]
    private Sprite m_CheckmarkSprite;
    [SerializeField]
    private Sprite m_TransparentSprite;

    private Image m_Image;

    void Awake ()
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = m_LockTopRightSprite;
    }

    public void SetUnlocked()
    {
        m_Image.sprite = m_TransparentSprite;
    }

    public void SetCenterLocked()
    {
        m_Image.sprite = m_LockCenterSprite;
    }

    public void SetTopRightLocked ()
    {
        m_Image.sprite = m_LockTopRightSprite;
    }

    public void SetEquipped ()
    {
        m_Image.sprite = m_CheckmarkSprite;
    }
}
