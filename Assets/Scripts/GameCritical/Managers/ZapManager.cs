using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class ZapManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("starting number of rows per grid")]
        private int m_Rows = 8;
        [SerializeField]
        [Tooltip("rows incremented per level")]
        private int m_RowsIncrement = 4;

        [SerializeField]
        [Tooltip("starting number of cols per grid")]
        private int m_Cols = 3;
        [SerializeField]
        [Tooltip("cols incremented per level")]
        private int m_ColsIncrement = 2;

        [SerializeField]
        [Tooltip("starting number of grids per stage")]
        private int m_Grids = 5;
        [SerializeField]
        [Tooltip("grids incremented per stage")]
        private int m_GridsIncrement = 0;
        private int m_CurrGrid = 1;


        [SerializeField]
        private List<ZapGrid> m_ZapGridsLvl1;

        private int m_ZapGridIndex;
        private ZapGrid m_CurrZapGrid;

        void Start()
        {
            m_ZapGridIndex = 0;
            SpawnNextZapGrid();
        }

        private void scaleDifficulty()
        {
            if (m_CurrGrid <= m_Grids)
            {
                levelScale();
            }
            else
            {
                stageScale();
                m_CurrGrid = 1;
            }
        }

        // scales difficulty after a stage
        private void stageScale()
        {
            m_Grids += m_GridsIncrement;
        }

        // scales difficulty after a level
        private void levelScale()
        {
            m_Rows += m_RowsIncrement;
            m_Cols += m_ColsIncrement;
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
                m_CurrZapGrid.Init(m_Rows, m_Cols);
                GameMaster.Instance.m_PlayerMovement.MoveToZapGrid();
                m_ZapGridIndex++;

                // scale difficulty for next level
                m_CurrGrid++;
                scaleDifficulty();
            }
        }

        public ZapGrid GetZapGrid()
        {
            return m_CurrZapGrid;
        }
    }
}
