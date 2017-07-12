using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Player;


namespace DadEvents
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private float m_LaserChargeTime;
        [SerializeField]
        [Range(0,1.0f)]
        private float m_LaserLength;
        [SerializeField]
        private float m_SlowMultiplier = 0.75f;
        [SerializeField]
        private float m_ShootTime;
        private float m_CurrShootTime;

        private LineRenderer m_LineRenderer;
        private SpriteRenderer m_SpriteRenderer;
        private BoxCollider2D m_BoxCollider2D;

        private Vector3 m_TargetPosition;

        private bool m_IsShooting;
        private bool m_IsOnRight;

        void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_LineRenderer = GetComponent<LineRenderer>();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            m_CurrShootTime = 0;
            m_LineRenderer.SetPosition(0, this.transform.position);
            m_LineRenderer.SetPosition(1, this.transform.position);
            m_TargetPosition = this.transform.position;
        }

        void Start()
        {
            StartCoroutine(charge());
        }

        public void SetPositionLaserPost(Vector3 pos, bool onRightSide)
        {
            m_IsOnRight = onRightSide;

            this.transform.position = pos;
            m_TargetPosition = pos;

            // get vector based on screen percentage laser takes up
            Vector3 laserVector = ScreenUtilities.GetWSofSSPosition(m_LaserLength, 0.0f);
            laserVector.z = 0;
            laserVector.y = 0;

            // orient laser based on if it is on left or right side of screen.
            if (onRightSide)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 180.0f);
                m_TargetPosition += laserVector;
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                m_TargetPosition -= laserVector;
            }

            // move original position of laser
            m_LineRenderer.SetPosition(0, this.transform.position);
            m_LineRenderer.SetPosition(1, this.transform.position);
        }

        private IEnumerator charge()
        {
            yield return new WaitForSeconds(m_LaserChargeTime);
            shootLaser();
        }

        private void shootLaser()
        {
            m_IsShooting = true;
        }

        private void slowPlayerIfInEffectZone()
        {
            PlayerMovement playerMovement = GameCritical.GameMaster.Instance.m_PlayerMovement;
            Vector3 playerPos = playerMovement.transform.position;
            Vector3 laserPos = this.transform.position;
            if (playerPos.y == laserPos.y)
            {
                Vector3 pos1 = m_LineRenderer.GetPosition(0);
                Vector3 pos2 = m_LineRenderer.GetPosition(1);
                Vector3 lineVect = pos2 - pos1;

                // check if vectors are parallel
                Vector3 normLineVect = lineVect.normalized;
                // make sure player is within range of effect
                if (!m_IsOnRight && (playerPos.x >= this.transform.position.x && playerPos.x <= this.transform.position.x + lineVect.magnitude))
                {
                    Debug.Log("EFFECT PLAYER FROM LEFT");
                }
                else if (m_IsOnRight && (playerPos.x <= this.transform.position.x && playerPos.x <= this.transform.position.x - lineVect.magnitude))
                {
                    Debug.Log("EFFECT PLAYER FROM RIGHT");
                }
            }
        }

        private void positionBoxCollider2D()
        {
            Vector3 localSpaceLineRendererVector = this.transform.InverseTransformPoint(m_LineRenderer.GetPosition(1)) - this.transform.InverseTransformPoint(m_LineRenderer.GetPosition(0));
            Vector3 middleOfLineRenderer = localSpaceLineRendererVector / 2.0f;
            m_BoxCollider2D.offset = new Vector2(middleOfLineRenderer.x, middleOfLineRenderer.y);
            m_BoxCollider2D.size = new Vector2(localSpaceLineRendererVector.magnitude, m_BoxCollider2D.size.y);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player")
            {
                PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
                if(playerMovement != null)
                {
                    playerMovement.SetSpeedMultiplier(m_SlowMultiplier, true);
                }
            }
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.SetSpeedMultiplier(1.0f, true);
                }
            }
        }

        void Update()
        {
            if(m_IsShooting)
            {
                m_CurrShootTime += Time.deltaTime;
                float shootPercentage = m_CurrShootTime / m_ShootTime;
                Vector3 currVector = Vector3.Lerp(this.transform.position, m_TargetPosition, shootPercentage);
                m_LineRenderer.SetPosition(1, currVector);
                if(shootPercentage >= 1.0f)
                {
                    shootPercentage = 1.0f;
                    m_IsShooting = false;
                }
            }

            positionBoxCollider2D();
        }
    }
}