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
        private float m_ZDistanceFromCamera = 1.0f;
        [SerializeField]
        private float m_YDistanceFromCamera = 4.0f;

        [SerializeField]
        private Zap m_EndZapPrefab;
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

        private List<List<Zap>> m_ZapGrid;
        private Vector3 m_TopMiddle;

        void Start()
        {
            Init();
        }

        public Vector3 GetTopMiddle()
        {
            return m_TopMiddle;
        }

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
            bool hasSetStartingPosition = false;
            float zapWidth = .675f / m_Cols;
            Vector3 botLeftInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 origin = botLeftInWorldSpace + new Vector3(0, m_YDistanceFromCamera, 0);
            origin.z = m_ZDistanceFromCamera;
            this.transform.position = origin;

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

                        // Specify the top middle position of the zap grid
                        if(!hasSetStartingPosition)
                        {
                            hasSetStartingPosition = true;
                            m_TopMiddle = this.transform.position +
                                new Vector3(m_Cols * zap.Width / 2.0f, 0, 0);
                        }

                        // spawn things randomly on zap
                        // SpawnRandomObstacle(i, j, m_ChanceOfObstacle);
                    }
                }
            }
        }
    }
}
