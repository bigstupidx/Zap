using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapLine : MonoBehaviour {

    [SerializeField]
    [Tooltip("Distance the walls spawn away from camera")]
    private float m_DistFromCamera = 2.0f;

    private int m_NumZaps;
    public int NumZaps { get { return m_NumZaps; } }

    private int m_TargetZapIndex;
    public int TargetZapIndex { get { return m_TargetZapIndex; } }

    private int m_CurrZapIndex;
    public int CurrZapIndex { get { return m_CurrZapIndex; } }

    private bool m_IsCountingUp;
    public bool IsCountingUp { get { return m_IsCountingUp; } }

    private List<Zap> m_Zaps;

    void Awake()
    {
        InitializeVariables(0, 0, true);
    }

    public Zap GetNextZap()
    {
        if(m_Zaps.Count > 0)
        {
            m_CurrZapIndex = m_TargetZapIndex;
            m_TargetZapIndex = (m_IsCountingUp) ? m_TargetZapIndex + 1 : m_TargetZapIndex - 1;
            if (m_TargetZapIndex >= m_Zaps.Count - 1)
            {
                m_IsCountingUp = false;
            }
            else if (m_TargetZapIndex <= 0)
            {
                m_IsCountingUp = true;
            }
            return m_Zaps[m_TargetZapIndex];
        }
        return null;
    }

    public Zap GetCurrentZap()
    {
        if (m_Zaps.Count > 0)
        {
            return m_Zaps[m_CurrZapIndex];
        }
        return null;
    }

    public void InitializeVariables(int currZapIndex, int targetZapIndex, bool isCountingUp)
    {
        m_CurrZapIndex = currZapIndex;
        m_TargetZapIndex = targetZapIndex;
        m_IsCountingUp = isCountingUp;
    }

    public int GetCurrZapIndex()
    {
        return m_CurrZapIndex;
    }
    public int GetTargetZapIndex()
    {
        return m_TargetZapIndex;
    }
    public bool GetIsCountingUp()
    {
        return m_IsCountingUp;
    }

    public void SpawnZaps(int numZaps, Vector3 position, float positionOffsetDistance, float zapWaitTime)
    {
        m_Zaps = new List<Zap>();
        m_NumZaps = numZaps;
        Vector3 spawnPosWorldSpace = Vector3.zero;
        float zapWidthWorldSpace = .675f / m_NumZaps;
        for (int i = 0; i < m_NumZaps; i++)
        {
            // set position accordingly
            if (m_Zaps.Count > 0)
            {
                Zap prevZap = m_Zaps[m_Zaps.Count - 1];
                spawnPosWorldSpace = prevZap.transform.position + new Vector3(prevZap.Width, 0, 0);
            }
            else
            {
                Vector3 startPosScreenSpace = new Vector3(0, 0, 0);
                spawnPosWorldSpace = position; // Camera.main.ScreenToWorldPoint(startPosScreenSpace);
                spawnPosWorldSpace.z = m_DistFromCamera;
            }

            // spawn zap
            Zap zapPrefab = GameMaster.Instance.m_ZapManager.GetRandomZapPrefab();
            Zap zap = (Zap)Instantiate(zapPrefab, this.transform, true);
            zap.transform.position = spawnPosWorldSpace;
            zap.SetWidth(zapWidthWorldSpace);
            zap.SetOffsetDistance(positionOffsetDistance);
            m_Zaps.Add(zap);
        }
    }
}
