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

        [SerializeField]
        private ParticleSystem m_OutterPS;
        [SerializeField]
        private Blink m_LaserBlinkPrefab;
        private Blink m_LaserBlinkInstance;

        private LineRenderer m_LineRenderer;
        private SpriteRenderer m_SpriteRenderer;
        private BoxCollider2D m_BoxCollider2D;

        private Vector3 m_TargetPosition;
        
        private bool m_IsOnRight;

        void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_LineRenderer = GetComponent<LineRenderer>();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
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
            Vector3 centerScreenVector = ScreenUtilities.GetWSofSSPosition(0.0f, 0.0f);
            laserVector = centerScreenVector - laserVector;

            m_LaserBlinkInstance = Instantiate(
                m_LaserBlinkPrefab,
                this.transform.position,
                Quaternion.identity,
                this.transform);

            // orient laser based on if it is on left or right side of screen.
            if (onRightSide)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 180.0f);
                m_LaserBlinkInstance.transform.localRotation = Quaternion.Euler(0, 0, 180.0f);
                m_TargetPosition += laserVector;
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                m_LaserBlinkInstance.transform.localRotation = Quaternion.Euler(0, 0, 180.0f);
                m_TargetPosition -= laserVector;
            }

            // move original position of laser
            m_LineRenderer.SetPosition(0, this.transform.position);
            m_LineRenderer.SetPosition(1, this.transform.position);
        }

        private IEnumerator charge()
        {
            StartCoroutine(m_LaserBlinkInstance.ChangeBlinkSpeed(m_LaserBlinkInstance, m_LaserChargeTime / 1.5f));

            yield return new WaitForSeconds(m_LaserChargeTime);

            // destroy blinking laser icon
            Destroy(m_LaserBlinkInstance.gameObject);

            shootLaser();
        }

        private void shootLaser()
        {
            m_OutterPS.Play();
            StartCoroutine(shootOut());
        }

        private void positionBoxCollider2D()
        {
            Vector3 localSpaceLineRendererVector = this.transform.InverseTransformPoint(m_LineRenderer.GetPosition(1)) - this.transform.InverseTransformPoint(m_LineRenderer.GetPosition(0));
            Vector3 middleOfLineRenderer = localSpaceLineRendererVector / 2.0f;
            m_BoxCollider2D.offset = new Vector2(middleOfLineRenderer.x, middleOfLineRenderer.y);
            m_BoxCollider2D.size = new Vector2(localSpaceLineRendererVector.magnitude, m_BoxCollider2D.size.y);
            m_OutterPS.transform.localPosition = localSpaceLineRendererVector;
        }

        public void OnTriggerStay2D(Collider2D col)
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

        private IEnumerator shootOut()
        {
            float currShootTime = 0;
            float shootPercentage = 0;

            while (shootPercentage < 1.0f)
            {
                currShootTime += Time.deltaTime;
                shootPercentage = currShootTime / m_ShootTime;
                Vector3 currVector = Vector3.Lerp(this.transform.position, m_TargetPosition, shootPercentage);
                m_LineRenderer.SetPosition(1, currVector);
                yield return null;
            }

            StartCoroutine(shootIn());
        } 

        private IEnumerator shootIn()
        {
            float currShootTime = 0;
            float shootPercentage = 0;

            while (shootPercentage < 1.0f)
            {
                currShootTime += Time.deltaTime;
                shootPercentage = currShootTime / m_ShootTime;
                Vector3 currVector = Vector3.Lerp(m_TargetPosition, this.transform.position, shootPercentage);
                m_LineRenderer.SetPosition(1, currVector);
                yield return null;
            }

            m_OutterPS.Stop();
            Destroy(this.gameObject);
        }

        void Update()
        {
            positionBoxCollider2D();
        }
    }
}