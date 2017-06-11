using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class ZapManager : MonoBehaviour
    {
        [SerializeField]
        private List<ZapGrid> m_ZapGridsLvl1;

        private int m_ZapGridIndex;
        private ZapGrid m_CurrZapGrid;

        void Start()
        {
            m_ZapGridIndex = 0;
            SpawnNextZapGrid();
        }

        public void SpawnNextZapGrid()
        {
            if(m_CurrZapGrid != null)
            {
                Destroy(m_CurrZapGrid);
            }

            ZapGrid zapGridPrefab = m_ZapGridsLvl1[m_ZapGridIndex % m_ZapGridsLvl1.Count];
            if(zapGridPrefab != null)
            {
                m_CurrZapGrid = (ZapGrid)Instantiate(zapGridPrefab);
                m_CurrZapGrid.Init();
                GameMaster.Instance.m_PlayerMovement.MoveToZapGrid();
                m_ZapGridIndex++;
            }
        }

        public ZapGrid GetZapGrid()
        {
            return m_CurrZapGrid;
        }
    }
}
