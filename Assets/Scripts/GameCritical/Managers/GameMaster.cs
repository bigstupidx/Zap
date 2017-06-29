﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using UI;
using Database;
using DadEvents;

namespace GameCritical
{
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster Instance;

        public ZapManager m_ZapManager;
        public UIManager m_UIManager;
        public WarpZoneManager m_WarpZoneManager;
        public PlayerMovement m_PlayerMovement;
        public PlayerDecorations m_PlayerDecorations;
        public PlayerStats m_PlayerStats;
        public BackdropManager m_BackDropManager;
        public StatsManager m_StatsManager;
        public DatabaseManager m_DatabaseManager;
        public DadEventManager m_DadEventManager;

        public DeathStar m_DeathStar;
        public CameraFollow m_CameraFollow;
        public ParticleSystem m_WarpParticleSystem;

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

            if (m_PlayerDecorations == null)
            {
                m_PlayerDecorations = FindObjectOfType<PlayerDecorations>();
            }
            if (m_DatabaseManager == null)
            {
                m_DatabaseManager = FindObjectOfType<DatabaseManager>();
            }
            if (m_PlayerMovement == null)
            {
                m_PlayerMovement = FindObjectOfType<PlayerMovement>();
            }
            if (m_UIManager == null)
            {
                m_UIManager = FindObjectOfType<UIManager>();
            }
            if (m_DeathStar == null)
            {
                m_DeathStar = FindObjectOfType<DeathStar>();
            }
            if(m_CameraFollow == null)
            {
                m_CameraFollow = FindObjectOfType<CameraFollow>();
            }
            if(m_WarpZoneManager == null)
            {
                m_WarpZoneManager = FindObjectOfType<WarpZoneManager>();
            }
            if (m_BackDropManager == null)
            {
                m_BackDropManager = FindObjectOfType<BackdropManager>();
            }
            if (m_StatsManager == null)
            {
                m_StatsManager = FindObjectOfType<StatsManager>();
            }
            if (m_PlayerStats == null)
            {
                m_PlayerStats = FindObjectOfType<PlayerStats>();
            }
            if (m_DadEventManager == null)
            {
                m_DadEventManager = FindObjectOfType<DadEventManager>();
            }
        }
    }
}
