using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class UIManager : MonoBehaviour {

    public ZapScore m_ZapScore;

    [SerializeField]
    private PopUpText m_PopUpTextPrefab;
    [SerializeField]
    private Vector3 m_PopUpTextOffset;

    // Use this for initialization
    void Awake()
    {
        if (m_ZapScore == null)
        {
            m_ZapScore = FindObjectOfType<ZapScore>();
        }
    }

    public void SpawnPopUpText(string str, Vector3 position, Color color)
    {
        PopUpText popUpTextPrefab = (PopUpText)Instantiate(m_PopUpTextPrefab, 
            position + m_PopUpTextOffset, 
            Quaternion.identity);
        popUpTextPrefab.SetText(str);
        popUpTextPrefab.SetColor(color);
    }
}
