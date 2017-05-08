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
        private ZapGrid m_CurrZapGrid;

        void Start()
        {
            SpawnNextZapGrid();
        }

        public void SpawnNextZapGrid()
        {
            if(m_CurrZapGrid != null)
            {
                Destroy(m_CurrZapGrid);
            }

            m_CurrZapGrid = (ZapGrid)Instantiate(m_ZapGrid);
            GameMaster.Instance.m_PlayerMovement.EnterZapGrid();
        }

        public ZapGrid GetZapGrid()
        {
            return m_CurrZapGrid;
        }
    }
}
