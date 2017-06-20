using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ShopCanvas : MonoBehaviour
    {
        public WarpStorePanel m_WarpStorePanel;
        public GameObject m_FailedPurchaseModal;

        // Use this for initialization
        void Start()
        {
            if (m_WarpStorePanel == null)
            {
                m_WarpStorePanel = FindObjectOfType<WarpStorePanel>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
