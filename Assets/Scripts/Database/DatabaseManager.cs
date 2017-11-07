using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class DatabaseManager : MonoBehaviour
    {
        public DataLoader m_DataLoader;
        public DataInserter m_DataInserter;

        // Use this for initialization
        void Start()
        {
            if(m_DataInserter == null)
            {
                m_DataInserter = FindObjectOfType<DataInserter>();
            }
            if (m_DataLoader == null)
            {
                m_DataLoader = FindObjectOfType<DataLoader>();
            }
        }
    }
}
