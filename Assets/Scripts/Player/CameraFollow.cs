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
        private Vector3 m_ShakeOffset;

        [SerializeField]
        private float m_LerpTime;
        private float m_CurrLerpTime;
        private float m_LerpAmount;
        
        [SerializeField]
        private bool m_ShakeHorizontal = true;
        [SerializeField]
        private float m_ShakeAmount = 5.0f;
        [SerializeField]
        private float m_ShakeSpeed = 4.0f;
        [SerializeField]
        private int m_NumShakesPerShake = 3;
        private int m_NumShakes;

        void Awake()
        {
            if (m_PlayerMovement == null)
            {
                m_PlayerMovement = FindObjectOfType<PlayerMovement>();
            }
            if(m_PlayerMovement)
            {
                Debug.Log("PLAYER MOVEMET NOT NULL)");
            }
        }

        // Use this for initialization
        void Start()
        {
            m_LerpAmount = 0.0f;
            m_CurrOffset = m_Offset;
            m_CurrLerpTime = m_LerpTime;
            m_NumShakes = 0;
        }

        public void SetOffset(Vector3 offset, float timeToTargetOffset)
        {
            m_CurrLerpTime = timeToTargetOffset;
            m_CurrOffset = offset;
            m_LerpAmount = 0.0f;
        }

        public void ResetOffset(float timeToTargetOffset)
        {
            SetOffset(m_Offset, timeToTargetOffset);
        }

        void LateUpdate()
        {
            if (m_PlayerMovement != null)
            {
                m_LerpAmount += Time.deltaTime;
                float lerpPercentage = m_LerpAmount / m_CurrLerpTime;

                Vector3 target = m_PlayerMovement.transform.position;
                target.x = 0;
                target.z = this.transform.position.z;
                this.transform.position = Vector3.Lerp(
                    this.transform.position, 
                    target + m_CurrOffset + m_ShakeOffset, 
                    lerpPercentage);

                // Once we lerp to position then follow target 100% until ordered to move somewhere else
                if (lerpPercentage >= 1.0f)
                {
                    m_CurrLerpTime = m_LerpTime;
                    //m_LerpAmount = 0.0f;
                }
            }
        }

        public void Shake()
        {
            Shake(m_NumShakesPerShake);
        }

        public void Shake(int numShakes)
        {
            StartCoroutine(shake());
        }

        private IEnumerator shake()
        {
            // initialization
            Vector3 targetPosition = (m_ShakeHorizontal) ? 
                new Vector3(m_ShakeAmount, 0, 0) :
                new Vector3(0, m_ShakeAmount, 0);
            float lerpPercentage = 0.0f;

            // move right
            while (lerpPercentage < 1.0f)
            {
                lerpPercentage += m_ShakeSpeed * Time.deltaTime;
                m_ShakeOffset = Vector3.Lerp(Vector3.zero, targetPosition, lerpPercentage);
                yield return null;
            }

            lerpPercentage = 0.0f;

            // move to center
            while (lerpPercentage < 1.0f)
            {
                lerpPercentage += m_ShakeSpeed * Time.deltaTime;
                m_ShakeOffset = Vector3.Lerp(targetPosition, Vector3.zero, lerpPercentage);
                yield return null;
            }
            
            lerpPercentage = 0.0f;

            // move to left
            while (lerpPercentage < 1.0f)
            {
                lerpPercentage += m_ShakeSpeed * Time.deltaTime;
                m_ShakeOffset = Vector3.Lerp(Vector3.zero, -targetPosition, lerpPercentage);
                yield return null;
            }

            lerpPercentage = 0.0f;

            // move to center
            while (lerpPercentage < 1.0f)
            {
                lerpPercentage += m_ShakeSpeed * Time.deltaTime;
                m_ShakeOffset = Vector3.Lerp(-targetPosition, Vector3.zero, lerpPercentage);
                yield return null;
            }

            // shake again if needed
            m_NumShakes--;
            if (m_NumShakes > 0)
            {
                StartCoroutine(shake());
            }
        }
    }
}