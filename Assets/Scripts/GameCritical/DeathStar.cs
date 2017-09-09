using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.UI;

namespace GameCritical
{
    public class DeathStar : MonoBehaviour
    {

        public bool m_CanKillPlayer;

        [SerializeField]
        private float m_Speed;

        [SerializeField]
        private Vector3 m_StartOffsetPosition = new Vector3(0, -1.0f, 0);

        [SerializeField]
        private ParticleSystem m_ParticleSystem;

        private float m_SpeedMultiplier;
        private bool m_IsMoving;

        private Player.PlayerMovement m_PlayerMovement;
        private Player.PlayerStats m_PlayerStats;
        private float m_PlayerColliderRadius;

        void Awake()
        {
            if (m_ParticleSystem == null)
            {
                m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
            }
        }

        void Start()
        {
            m_SpeedMultiplier = 1.0f;
            m_IsMoving = true;
            m_PlayerMovement = GameMaster.Instance.m_PlayerMovement;
            m_PlayerStats = GameMaster.Instance.m_PlayerStats;
            CircleCollider2D playerCircleCollider = m_PlayerMovement.GetComponent<CircleCollider2D>();
            if(playerCircleCollider != null)
            {
                m_PlayerColliderRadius = playerCircleCollider.radius;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(m_IsMoving)
            {
                this.transform.position += new Vector3(0, m_Speed, 0) * Time.deltaTime * m_SpeedMultiplier;
            }

            // Kill player if death star overlaps
            if(m_CanKillPlayer)
            {
                if (m_PlayerMovement != null && m_PlayerStats != null)
                {
                    float playerY = m_PlayerMovement.transform.position.y;
                    if (playerY - m_PlayerColliderRadius < this.transform.position.y)
                    {
                        m_PlayerStats.Kill();
                        SetIsMoving(false);
                    }
                }
            }
        }

        public void ResetPosition()
        {
            Vector3 bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
            bottomOfScreen.z = 0;
            this.transform.position = bottomOfScreen + m_StartOffsetPosition;
            SetSpeedMultiplier(1.0f, false);
        }

        public void SetIsMoving(bool isMoving)
        {
            m_IsMoving = isMoving;
            if (isMoving)
                m_ParticleSystem.Play();
            else
                m_ParticleSystem.Stop();
        }

        public void SetSpeedMultiplier(float multiplier, bool shouldAddToCurrentSpeed)
        {
            m_SpeedMultiplier = (shouldAddToCurrentSpeed) ? m_SpeedMultiplier + multiplier: multiplier;
            GameMaster.Instance.m_UIManager.m_InfoPanel.SetDeathStarMultiplierText(m_SpeedMultiplier);
        }
    }
}