using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Zap : MonoBehaviour
    {
        [SerializeField]
        private Color m_PopUpColor;

        public bool m_HasPoints;
        public int m_Points;

        public bool m_HasPopUpText;
        public string m_PopUpText;

        [SerializeField]
        protected ParticleSystem m_ActiveParticleSystem;

        [SerializeField]
        private Color m_Color;

        public float Width { get { return m_SpriteRenderer.bounds.size.x; } }
        public float Height { get { return m_SpriteRenderer.bounds.size.y; } }
        private Vector3 m_OffsetPosition;
        private int m_Row;
        public int Row { get { return m_Row; } set { m_Row = value; } }
        private int m_Col;
        public int Col { get { return m_Col; } set { m_Col = value; } }

        protected SpriteRenderer m_SpriteRenderer;

        void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = m_Color;
        }

        void Start()
        {
            centerParticleSystem();
        }

        private void centerParticleSystem()
        {
            if (m_ActiveParticleSystem)
            {
                m_ActiveParticleSystem.transform.position = this.transform.position + new Vector3(this.Width / 2.0f, this.Height / 2.0f, 0);
            }
        }

        public virtual void ApplyImmediateEffect() { }

        public virtual void ApplyCollisionEffect(GameObject go)
        {
            AddAndShowPoints();
            ShowPopUptext();
        }

        public void AddAndShowPoints()
        {
            if (m_HasPoints)
            {
                UIManager m_UIManager = GameMaster.Instance.m_UIManager;
                StatsManager m_StatsManager = GameMaster.Instance.m_StatsManager;
                if (m_UIManager && m_StatsManager)
                {
                    m_StatsManager.AddToScore(m_Points);
                    m_UIManager.SpawnPopUpText(
                        m_Points.ToString(),
                        this.transform.position + new Vector3(Width / 2.0f, 0, 0),
                        m_Color);
                }
            }
        }

        public void ShowPopUptext()
        {
            if (m_HasPopUpText)
            {
                GameMaster.Instance.m_UIManager.SpawnPopUpText(m_PopUpText,
                    this.transform.position + new Vector3(Width / 2.0f, 0, 0),
                    m_PopUpColor);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                ApplyCollisionEffect(col.gameObject);
            }
        }

        public void SetOffsetDistance(float distanceFromZap)
        {
            m_OffsetPosition = new Vector3(Width / 2.0f, Height / 2.0f - distanceFromZap, 0) + this.transform.position;
        }

        public Vector3 GetOffsetPosition()
        {
            return m_OffsetPosition;
        }

        public virtual void SetWidth(float width)
        {
            Vector3 currScale = this.transform.localScale;
            this.transform.localScale = new Vector3(width, currScale.y, currScale.z);
            centerParticleSystem();
        }

        public virtual void SetHeight(float height)
        {
            Vector3 currScale = this.transform.localScale;
            this.transform.localScale = new Vector3(currScale.x, height, currScale.z);
            centerParticleSystem();
        }
    }
}
