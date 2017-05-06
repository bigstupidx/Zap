using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapManager : MonoBehaviour {

    [SerializeField]
    private List<Zap> m_ZapPrefabs;
    private ZapGrid m_ZapGrid;

    // Use this for initialization
    void Start ()
    {
        int rows = 60;
        int cols = 3;
        float offsetDistance = 0.7f;
        float rowGapDistance = 1.5f;
        Vector3 screenBottomWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        m_ZapGrid = new ZapGrid();
        Zap randomPrefab = GetRandomZapPrefab();
        m_ZapGrid.Init(screenBottomWorldSpace, rows, cols, 1.0f, rowGapDistance, offsetDistance);
    }

    public Zap GetRandomZapPrefab()
    {
        int index = Mathf.FloorToInt(Random.Range(0, m_ZapPrefabs.Count));
        return m_ZapPrefabs[index];
    }

    public ZapGrid GetZapGrid()
    {
        return m_ZapGrid;
    }
}
