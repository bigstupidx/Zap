using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

namespace GameCritical
{
    public class ZapManager : MonoBehaviour
    {
        [SerializeField]
        private List<Zap> m_ZapPrefabs;
        [SerializeField]
        private List<Obstacle> m_ObstaclePrefabs;

        [SerializeField]
        private int m_Rows = 60;
        [SerializeField]
        private int m_Cols = 5;
        [SerializeField]
        private float m_OffsetDistance = 0.7f;
        [SerializeField]
        private float m_RowGapDistance = 1.5f;
        [SerializeField]
        [Tooltip("Chance of obstacle spawning per Zap")]
        private float m_ChanceOfObstacle = 0.05f;

        private ZapGrid m_ZapGrid;

        // Use this for initialization
        void Start()
        {
            Vector3 screenBottomWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            m_ZapGrid = new ZapGrid();
            Zap randomPrefab = GetRandomZapPrefab();
            m_ZapGrid.Init(screenBottomWorldSpace, m_Rows, m_Cols, 1.0f, 
                m_RowGapDistance, m_OffsetDistance, m_ChanceOfObstacle);
        }

        public Zap GetRandomZapPrefab()
        {
            int index = Mathf.FloorToInt(Random.Range(0, m_ZapPrefabs.Count));
            return m_ZapPrefabs[index];
        }

        public Obstacle GetRandomObstaclePrefab()
        {
            int index = Mathf.FloorToInt(Random.Range(0, m_ObstaclePrefabs.Count));
            return m_ObstaclePrefabs[index];
        }

        public ZapGrid GetZapGrid()
        {
            return m_ZapGrid;
        }
    }
}
