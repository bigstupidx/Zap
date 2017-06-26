using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class ZapManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("starting number of rows per grid")]
        private int m_StartRows = 8;
        [SerializeField]
        [Tooltip("starting number of cols per grid")]
        private int m_StartCols = 3;
        [SerializeField]
        [Tooltip("starting number of grids per stage")]
        private int m_StartGrids = 1;

        [SerializeField]
        [Tooltip("rows incremented per level")]
        private int m_RowsIncrement = 4;
        private int m_CurrRows; // current number of rows in the grid we are on
        [SerializeField]
        [Tooltip("cols incremented per level")]
        private int m_ColsIncrement = 2;
        private int m_CurrCols; // current number of cols in the grid we are on
        [SerializeField]
        [Tooltip("grids incremented per stage")]
        private int m_GridsIncrement = 0;
        private int m_CurrGrids = 0; // total number of grids in this stage
        private int m_CurrGridIndex = 0; // current grid that we are on

        [SerializeField]
        private ZapGrid m_ZapGridPrefab;
        private ZapGrid m_CurrZapGrid;

        void Start()
        {
            m_CurrCols = m_StartCols;
            m_CurrRows = m_StartRows;
            m_CurrGrids = m_StartGrids;
            m_CurrGridIndex = 0;
            SpawnNextZapGrid();
        }

        private void scaleDifficulty()
        {
            if (m_CurrGridIndex + 1 < m_CurrGrids)
            {
                levelScale();
            }
            else
            {
                stageScale();
            }
        }

        // scales difficulty after a stage
        private void stageScale()
        {
            m_CurrGrids += m_GridsIncrement;
            m_CurrRows = m_StartRows;
            m_CurrCols = m_StartCols;
            m_CurrGridIndex = 0;
            GameMaster.Instance.m_BackDropManager.ShowNextStageColors();
        }

        // scales difficulty after a level
        private void levelScale()
        {
            m_CurrGridIndex++;
            m_CurrRows += m_RowsIncrement;
            m_CurrCols += m_ColsIncrement;
        }

        public void SpawnNextZapGrid()
        {
            if(m_CurrZapGrid != null)
            {
                Destroy(m_CurrZapGrid);
                // scale difficulty for next level
                scaleDifficulty();
            }

            if (m_ZapGridPrefab != null)
            {
                m_CurrZapGrid = (ZapGrid)Instantiate(m_ZapGridPrefab);
                m_CurrZapGrid.Init(m_CurrRows, m_CurrCols);
                GameMaster.Instance.m_PlayerMovement.MoveToZapGrid();
            }
        }

        public ZapGrid GetZapGrid()
        {
            return m_CurrZapGrid;
        }
    }
}
