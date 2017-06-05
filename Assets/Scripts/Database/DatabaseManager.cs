using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class DatabaseManager : MonoBehaviour
    {
        public DataLoader m_DataLoader;
        public DataInserter m_DataInserter;
        public Login m_Login;

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
            if (m_Login == null)
            {
                m_Login = FindObjectOfType<Login>();
            }
        }
    }
}
