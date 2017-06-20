﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utlities;

namespace GameCritical
{
    public class ZapGrid : MonoBehaviour
    {
        [SerializeField]
        private int m_Rows = 60;
        [SerializeField]
        private int m_Cols = 5;
        [SerializeField]
        private float m_ZapHeight = 0.1f;
        [SerializeField]
        private float m_OffsetDistance = 0.7f;
        [SerializeField]
        private float m_RowGapDistance = 1.5f;

        [SerializeField]
        private Zap m_EndZapPrefab;
        [SerializeField]
        private List<Zap> m_ZapPrefabs;
        [SerializeField]
        private List<float> m_ZapPrefabChance;
        [SerializeField]
        private List<int> m_ZapPrefabsForced;

        private List<List<Zap>> m_ZapGrid;

        public int GetNumCols(int row)
        {
            if(m_ZapGrid != null)
            {
                return m_ZapGrid[row].Count;
            }
            return 0;
        }

        public int GetNumRows()
        {
            if (m_ZapGrid != null)
            {
                return m_ZapGrid.Count;
            }
            return 0;
        }

        public Zap GetZap(int row, int col)
        {
            if (m_ZapGrid != null)
            {
                if (row < m_ZapGrid.Count && col < m_ZapGrid[row].Count && col >= 0)
                {
                    return m_ZapGrid[row][col];
                }
            }
            return null;
        }

        public Zap GetRandomZapPrefab()
        {
            if (m_ZapPrefabs == null || m_ZapPrefabs.Count <= 0)
            {
                return null;
            }

            Zap resZapPrefab = null;
            float randPercent = Random.Range(0, 1.0f);
            float lowerBound = 0.0f;
            float upperBound = 1.0f;
            for (int i = 0; i < m_ZapPrefabChance.Count; i++)
            {
                upperBound = m_ZapPrefabChance[i] + lowerBound;
                if (randPercent >= lowerBound && randPercent <= upperBound)
                {
                    resZapPrefab = m_ZapPrefabs[i];
                }
                lowerBound = upperBound;
            }

            return resZapPrefab;
        }

        void OnDestroy()
        {
            Destroy(this.gameObject);
        }

        public void Init(int rows, int cols)
        {
            m_Rows = rows;
            m_Cols = cols;
            Init();
        }

        private float GetZapWidth(Zap zap, int numCols)
        {
            float height = Camera.main.orthographicSize * 2;
            float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio
            Sprite s = zap.GetComponent<SpriteRenderer>().sprite;
            float unitWidth = s.textureRect.width / s.pixelsPerUnit;
            float unitHeight = s.textureRect.height / s.pixelsPerUnit;
            return width / unitWidth / numCols;
        }

        public void Init()
        {
            // pre fill the zap grid before shuffling.
            m_ZapGrid = new List<List<Zap>>();
            for (int i = 0; i < m_Rows + 1; i++)
            {
                List<Zap> zapRowToFill = new List<Zap>();

                if(i >= m_Rows)
                {
                    for(int j = 0; j < m_Cols; j++)
                    {
                        zapRowToFill.Add(m_EndZapPrefab);
                    }
                }
                else
                { 
                    // spawn all required zaps for this row
                    int totalForcedPrefabsAdded = 0;
                    for (int j = 0; j < m_ZapPrefabsForced.Count; j++)
                    {
                        int numOfPrefabToSpawn = m_ZapPrefabsForced[j];
                        Zap prefabToSpawn = m_ZapPrefabs[j];
                        for (int k = 0; k < numOfPrefabToSpawn; k++)
                        {
                            zapRowToFill.Add(prefabToSpawn);
                            totalForcedPrefabsAdded++;
                        }
                    }

                    for (int j = totalForcedPrefabsAdded; j < m_Cols; j++)
                    {
                        // spawn zap
                        Zap zapPrefab = GetRandomZapPrefab();
                        zapRowToFill.Add(zapPrefab);
                    }

                    // shuffle the zaps in each row
                    for (int k = 0; k < zapRowToFill.Count; k++)
                    {
                        Zap temp = zapRowToFill[k];
                        int randomIndex = (int)Random.Range(0, zapRowToFill.Count - 1);
                        zapRowToFill[k] = zapRowToFill[randomIndex];
                        zapRowToFill[randomIndex] = temp;
                    }
                }

                m_ZapGrid.Add(zapRowToFill);
            }

            // Do one time calculations for grid before spawning zaps.
            Vector3 topLeftInWorldSpace = Camera.main.ScreenToWorldPoint(
                new Vector3(0, 1.5f * ScreenUtilities.GetDistanceInWS(1.0f), 0));
            Vector3 origin = topLeftInWorldSpace;
            origin.z = 1.0f;
            this.transform.position = origin;
            float zapWidth = GetZapWidth(m_EndZapPrefab, m_Cols);

            for (int i = 0; i < m_Rows + 1; i++)
            {
                Vector3 spawnPos = Vector3.zero;
                for (int j = 0; j < m_Cols; j++)
                {
                    // set position accordingly relative to previous zap.
                    if (j > 0)
                    {
                        Zap prevZap = m_ZapGrid[i][j - 1];
                        spawnPos = prevZap.transform.position + new Vector3(prevZap.Width, 0, 0);
                    }
                    else // spawn start zap in row
                    {
                        spawnPos = origin + new Vector3(0, i * m_RowGapDistance, 0);
                    }

                    Zap zapPrefab = m_ZapGrid[i][j];
                    if (zapPrefab != null)
                    {
                        Zap zap = (Zap)Instantiate(zapPrefab, this.transform);
                        zap.transform.position = spawnPos;
                        zap.SetWidth(zapWidth);
                        zap.SetHeight(m_ZapHeight);
                        zap.SetOffsetDistance(m_OffsetDistance);
                        zap.Row = i;
                        zap.Col = j;
                        m_ZapGrid[i][j] = zap;

                        // spawn things randomly on zap
                        // SpawnRandomObstacle(i, j, m_ChanceOfObstacle);
                    }
                }
            }
        }
    }
}
