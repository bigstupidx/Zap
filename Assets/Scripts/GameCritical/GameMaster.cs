using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UI;

namespace GameCritical
{
    public class GameMaster : MonoBehaviour
    {

        public static GameMaster Instance;
        public ZapManager m_ZapManager;
        public UIManager m_UIManager;
        public PlayerMovement m_PlayerMovement;
        public ZapScore m_ZapScore;

        void Awake()
        {
            // create static instance if there is not one
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(this.gameObject);
                }
            }

            if (m_PlayerMovement == null)
            {
                m_PlayerMovement = FindObjectOfType<PlayerMovement>();
            }
            if (m_ZapScore == null)
            {
                m_ZapScore = FindObjectOfType<ZapScore>();
            }
            if (m_UIManager == null)
            {
                m_UIManager = FindObjectOfType<UIManager>();
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
