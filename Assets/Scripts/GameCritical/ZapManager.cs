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
        private DeadZone m_CurrDeadZone;

        // Use this for initialization
        void Start()
        {
            SpawnNextZapGrid();
        }

        public void EnterDeadZone()
        {
            m_CurrDeadZone = (DeadZone)Instantiate(m_DeadZonePrefab);
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
