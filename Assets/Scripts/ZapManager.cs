using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapManager : MonoBehaviour {

    [SerializeField]
    private List<Zap> m_ZapPrefabs;
    [SerializeField]
    private ZapLine m_ZapLinePrefab;
    private ZapLine m_CurrZapLine;
    [SerializeField]
    private float m_ZapWaitTime;

    private List<ZapLine> m_ZapLines;
    private int m_CurrZapLineIndex;

    // Use this for initialization
    void Start ()
    {
        m_ZapLines = new List<ZapLine>();
        Vector3 screenBottomWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        int numZapLines = 20;
        int numZapsPerLine = 3;
        float positionOffset = 0.7f;
        float zapGapDistance = 1.5f;
        m_CurrZapLineIndex = -1; // start at -1 because when GetNextZapline request will increment
        for (int i = 0; i < numZapLines; i++)
        {
            ZapLine zapLine = (ZapLine)Instantiate(m_ZapLinePrefab);
            Vector3 posOffset = new Vector3(0, 3 + i * zapGapDistance, 0);
            zapLine.SpawnZaps(numZapsPerLine, screenBottomWorldSpace + posOffset, positionOffset, m_ZapWaitTime);
            m_ZapLines.Add(zapLine);
        }
    }

    public Zap GetRandomZapPrefab()
    {
        int index = Mathf.FloorToInt(Random.Range(0, m_ZapPrefabs.Count));
        return m_ZapPrefabs[index];
    }

    public ZapLine GetNextZapLine()
    {
        if(m_CurrZapLineIndex + 1 < m_ZapLines.Count)
        {
            m_CurrZapLineIndex++;
            ZapLine prevZapLine = m_CurrZapLine;
            m_CurrZapLine = m_ZapLines[m_CurrZapLineIndex];
            // if there was a current zap line we were traversing then carry over the values
            if (prevZapLine != null && m_CurrZapLine != null)
            {
                m_CurrZapLine.SetTargetZapIndex(prevZapLine.GetTargetZapIndex()); // -1 because we are calling GetNextZap() below which will increment index then get zap
                m_CurrZapLine.SetCurrZapIndex(prevZapLine.GetTargetZapIndex());
                m_CurrZapLine.SetIsCountingUp(prevZapLine.GetIsCountingUp());
            }
            return m_CurrZapLine;
        }
        return null;
    }

    public Zap GetNextZap()
    {
        if (m_CurrZapLine == null)
        {
            m_CurrZapLine = GetNextZapLine();
            return m_CurrZapLine.GetCurrentZap();
        }
        else
        {
            return m_CurrZapLine.GetNextZap();
        }
    }

    public Zap GetCurrentZap()
    {
        if (m_CurrZapLine == null)
        {
            m_CurrZapLine = GetNextZapLine();
        }
        return m_CurrZapLine.GetCurrentZap();
    }
}
