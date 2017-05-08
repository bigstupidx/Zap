using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField]
        private PlayerMovement m_PlayerMovement;
        [SerializeField]
        private Vector3 m_Offset;
        private Vector3 m_CurrOffset;

        [SerializeField]
        private float m_LerpTime;
        private float m_CurrLerpTime;
        private float m_LerpAmount;

        // Use this for initialization
        void Start()
        {
            m_LerpAmount = 0.0f;
            m_CurrOffset = m_Offset;
            m_CurrLerpTime = m_LerpTime;

            if (m_PlayerMovement == null)
            {
                m_PlayerMovement = FindObjectOfType<PlayerMovement>();
            }
        }

        public void SetOffset(Vector3 offset, float timeToTargetOffset)
        {
            m_CurrLerpTime = timeToTargetOffset;
            m_CurrOffset = offset;
            m_LerpAmount = 0.0f;
        }

        public void ResetOffset(float timeToTargetOffset)
        {
            m_CurrLerpTime = timeToTargetOffset;
            m_CurrOffset = m_Offset;
            m_LerpAmount = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_PlayerMovement != null)
            {
                m_LerpAmount += Time.deltaTime;
                float lerpPercentage = m_LerpAmount / m_CurrLerpTime;

                // lerp to specified offset position so camera doesn't snap there
                //m_CurrOffset = Vector3.Lerp(this.m_PreviousOffset, this.m_CurrOffset, lerpPercentage);

                Vector3 target = m_PlayerMovement.transform.position;
                target.x = 0;
                target.z = 0;
                this.transform.position = Vector3.Lerp(this.transform.position, target + m_CurrOffset, lerpPercentage);

                if (lerpPercentage >= 1.0f)
                {
                    m_CurrLerpTime = m_LerpTime;
                    m_LerpAmount = 0.0f;
                }
            }
        }
    }
}