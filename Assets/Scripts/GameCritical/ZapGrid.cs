using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

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
        private List<Zap> m_ZapPrefabs;
        [SerializeField]
        private List<float> m_ZapPrefabChance;
        [SerializeField]
        private List<int> m_ZapPrefabsForced;

        [SerializeField]
        private List<Obstacle> m_ObstaclePrefabs;
        [SerializeField]
        [Tooltip("Chance of obstacle spawning per Zap")]
        private float m_ChanceOfObstacle = 0.05f;

        void Start()
        {
            Init();
        }

        private List<List<Zap>> m_ZapGrid;

        public int GetNumCols(int row)
        {
            return m_ZapGrid[row].Count;
        }

        public int GetNumRows()
        {
            return m_ZapGrid.Count;
        }

        public Zap GetZap(int row, int col)
        {
            if (row < m_ZapGrid.Count && col < m_ZapGrid[row].Count && col >= 0)
            {
                return m_ZapGrid[row][col];
            }
            return null;
        }

        public Obstacle GetRandomObstaclePrefab()
        {
            if (m_ObstaclePrefabs == null || m_ObstaclePrefabs.Count <= 0)
            {
                return null;
            }

            int index = Mathf.FloorToInt(Random.Range(0, m_ObstaclePrefabs.Count));
            return m_ObstaclePrefabs[index];
        }

        private void SpawnRandomObstacle(int row, int col, float chance)
        {
            if(Random.Range(0,1.0f) <= chance)
            {
                Obstacle randomObstacle = GetRandomObstaclePrefab();
                if(randomObstacle != null)
                {
                    Obstacle newObstacle = (Obstacle)Instantiate(randomObstacle);
                    newObstacle.SetPosition(row, col);
                }
            }
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

        public void Init()
        {
            // pre fill the zap grid before shuffling.
            m_ZapGrid = new List<List<Zap>>();
            for (int i = 0; i < m_Rows; i++)
            {
                List<Zap> zapRowToFill = new List<Zap>();

                // spawn all required zaps for this row
                int totalForcedPrefabsAdded = 0;
                for(int j = 0; j < m_ZapPrefabsForced.Count; j++)
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

                m_ZapGrid.Add(zapRowToFill);
            }

            // actually spawn the zap grid
            for (int i = 0; i < m_Rows; i++)
            {
                Vector3 spawnPos = Vector3.zero;
                float zapWidth = .675f / m_Cols;
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
                        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
                        float distanceFromCamera = 1.0f;
                        spawnPos = origin + new Vector3(0, i * m_RowGapDistance, 0);
                        spawnPos.z = distanceFromCamera;
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
