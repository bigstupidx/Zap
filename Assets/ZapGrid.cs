﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapGrid : MonoBehaviour {

    private Zap[][] m_ZapGrid;

	// Use this for initialization
	void Start () {

	}

    public int GetNumCols(int row)
    {
        return m_ZapGrid[row].Length;
    }

    public int GetNumRows()
    {
        return m_ZapGrid.Length;
    }

    public Zap GetZap(int row, int col)
    {
        if (row < m_ZapGrid.Length && col < m_ZapGrid[row].Length && col >= 0)
        {
            return m_ZapGrid[row][col];
        }
        return null;
    }

    public void Init(Vector3 origin, int rows, int cols, float distanceFromCamera, float rowGapDistance, float offsetDistance)
    {
        m_ZapGrid = new Zap[rows][];
        for(int i = 0; i < rows; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            float zapWidth = .675f / cols;
            m_ZapGrid[i] = new Zap[cols];
            for(int j = 0; j < cols; j++)
            {
                // set position accordingly relative to previous zap.
                if (j > 0)
                {
                    Zap prevZap = m_ZapGrid[i][j - 1];
                    spawnPos = prevZap.transform.position + new Vector3(prevZap.Width, 0, 0);
                }
                else // spawn start zap in row
                {
                    spawnPos = origin + new Vector3(0, i * rowGapDistance, 0);
                    spawnPos.z = distanceFromCamera;
                }

                // spawn zap
                Zap zapPrefab = GameMaster.Instance.m_ZapManager.GetRandomZapPrefab();
                Zap zap = (Zap)Instantiate(zapPrefab);
                zap.transform.position = spawnPos;
                zap.SetWidth(zapWidth);
                zap.SetRow(i);
                zap.SetCol(j);
                zap.SetOffsetDistance(offsetDistance);
                m_ZapGrid[i][j] = zap;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
