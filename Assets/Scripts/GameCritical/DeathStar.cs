using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class DeathStar : MonoBehaviour
    {

        public bool m_CanKillPlayer;

        [SerializeField]
        private float m_Speed;

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

        public void SetIsMoving(bool isMoving)
        {
            m_IsMoving = isMoving;
        }

        public void SetSpeedMultiplier(float multiplier, bool shouldMultiplyByCurrentSpeed)
        {
            m_SpeedMultiplier = (shouldMultiplyByCurrentSpeed) ? m_SpeedMultiplier * multiplier: multiplier;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                if (m_CanKillPlayer)
                {
                    SetIsMoving(false);
                    Destroy(col.gameObject);
                }
            }
        }
    }
}