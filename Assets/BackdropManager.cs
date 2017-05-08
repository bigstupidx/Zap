using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropManager : MonoBehaviour {

    [SerializeField]
    private Backdrop m_Backdrop;


    [SerializeField]
    private Color m_NormalColor1;
    [SerializeField]
    private Color m_NormalColor2;

    [SerializeField]
    private Color m_WarpStoreColor1;
    [SerializeField]
    private Color m_WarpStoreColor2;

    [SerializeField]
    private float m_LerpTime;
    private float m_LerpAmount;

    private Color m_PrevColor1;
    private Color m_PrevColor2;
    private Color m_NextColor1;
    private Color m_NextColor2;
    private bool m_IsLerping;

    void Start()
    {
        m_LerpAmount = 0.0f;
        m_IsLerping = false;
        if (m_Backdrop == null)
        {
            m_Backdrop = FindObjectOfType<Backdrop>();
        }
    }

    void Update()
    {
        if(m_IsLerping)
        {
            m_LerpAmount += Time.deltaTime;
            float m_LerpPercentage = m_LerpAmount / m_LerpTime;
            Color currColor1 = Color.Lerp(m_PrevColor1, m_NextColor1, m_LerpPercentage);
            Color currColor2 = Color.Lerp(m_PrevColor2, m_NextColor2, m_LerpPercentage);
            SetColors(currColor1, currColor2);

            if(m_LerpPercentage >= 1.0f)
            {
                m_IsLerping = false;
                m_LerpAmount = 0.0f;
            }
        }
    }

    private void ChangeColors(Color col1, Color col2)
    {
        m_LerpAmount = 0.0f;
        m_IsLerping = true;
        m_PrevColor1 = m_Backdrop.GetFirstColor();
        m_PrevColor2 = m_Backdrop.GetSecondColor();
        m_NextColor1 = col1;
        m_NextColor2 = col2;
    }

    public void ShowNormalColors()
    {
        ChangeColors(m_NormalColor1, m_NormalColor2);
    }

    public void ShowWarpStoreColors()
    {
        ChangeColors(m_WarpStoreColor1, m_WarpStoreColor2);
    }

    public void SetColors(Color col1, Color col2)
    {
        m_Backdrop.SetFirstColor(col1);
        m_Backdrop.SetSecondColor(col2);
    }
}
