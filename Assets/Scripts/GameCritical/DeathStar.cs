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

        private float m_SpeedMultiplier;
        private bool m_IsMoving;

        void Start()
        {
            m_SpeedMultiplier = 1.0f;
            m_IsMoving = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(m_IsMoving)
            {
                this.transform.position += new Vector3(0, m_Speed, 0) * Time.deltaTime * m_SpeedMultiplier;
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
        }

        public void SetSpeedMultiplier(float multiplier, bool shouldAddToCurrentSpeed)
        {
            m_SpeedMultiplier = (shouldAddToCurrentSpeed) ? m_SpeedMultiplier + multiplier: multiplier;
            GameMaster.Instance.m_UIManager.m_InfoPanel.SetDeathStarMultiplierText(m_SpeedMultiplier);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
                if (playerStats && m_CanKillPlayer)
                {
                    SetIsMoving(false);
                    playerStats.Kill();
                }
            }
        }
    }
}