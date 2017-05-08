using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

namespace GameCritical
{
    public class ZapManager : MonoBehaviour
    {
        [SerializeField]
        private ZapGrid m_ZapGrid;

        [SerializeField]
        private DeadZone m_DeadZonePrefab;
        private DeadZone m_DeadZone;

        // Use this for initialization
        void Start()
        {
            SpawnNextZapGrid();
        }

        public DeadZone SpawnDeadZone()
        {
            Vector3 spawnPos = m_ZapGrid.GetTopMiddle();
            m_DeadZone = (DeadZone)Instantiate(m_DeadZonePrefab, spawnPos + m_DeadZonePrefab.GetOriginOffsetPosition(), Quaternion.identity);
            return m_DeadZone;
        }

        public DeadZone GetDeadZone()
        {
            return m_DeadZone;
        }

        public void SpawnNextZapGrid()
        {
            m_ZapGrid = Instantiate(m_ZapGrid);
        }

        public ZapGrid GetZapGrid()
        {
            return m_ZapGrid;
        }
    }
}
