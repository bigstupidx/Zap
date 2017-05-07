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

        // Use this for initialization
        void Start()
        {
            m_ZapGrid = Instantiate(m_ZapGrid);
        }

        public ZapGrid GetZapGrid()
        {
            return m_ZapGrid;
        }
    }
}
