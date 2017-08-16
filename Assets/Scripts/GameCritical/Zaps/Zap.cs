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

        private bool m_Occupied;

        void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = m_Color;
            m_Occupied = false;
        }

        void Start()
        {
            centerParticleSystem();
        }

        public void SetOccupied(bool occupied)
        {
            m_Occupied = occupied;
        }

        public bool GetIsOccupied()
        {
            return m_Occupied;
        }

        private void centerParticleSystem()
        {
            if(m_ActiveParticleSystem)
            {
                centerObjectOnZap(m_ActiveParticleSystem.gameObject);
            }
        }

        public void centerObjectOnZap(GameObject go)
        {
            if (go != null)
            {
                go.transform.position = this.transform.position + new Vector3(this.Width / 2.0f, this.Height / 2.0f, 0);
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
                        this.transform.position + new Vector3(Width / 2.0f, Height * 2.0f, 0),
                        m_PopUpColor);
                }
            }
        }

        public void ShowPopUptext()
        {
            if (m_HasPopUpText)
            {
                GameMaster.Instance.m_UIManager.SpawnPopUpText(m_PopUpText,
                    this.transform.position + new Vector3(Width / 2.0f, Height * 2.0f, 0),
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

        public Vector3[] GetAllChildrenScales()
        {
            int numChildren = this.transform.childCount;
            Vector3[] scales = new Vector3[numChildren];
            for (int i = 0; i < numChildren; i++)
            {
                scales[i] = this.transform.GetChild(i).localScale;
            }
            return scales;
        }

        public void SetAllChildrenScales(Vector3 scaleRatios)
        {
            int numChildren = this.transform.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                Vector3 scale = this.transform.GetChild(i).localScale;
                this.transform.GetChild(i).localScale = new Vector3(
                    scale.x * scaleRatios.x, 
                    scale.y * scaleRatios.y,
                    scale.z * scaleRatios.z);
            }
        }

        public virtual void SetWidth(float width)
        {
            Vector3 currScale = this.transform.localScale;
            this.transform.localScale = new Vector3(width, currScale.y, currScale.z);
            Vector3 scaleRatios = new Vector3(
                currScale.x / this.transform.localScale.x,
                currScale.y / this.transform.localScale.y,
                currScale.z / this.transform.localScale.z);
            centerParticleSystem();
            SetAllChildrenScales(scaleRatios);
        }

        public virtual void SetHeight(float height)
        {
            Vector3 currScale = this.transform.localScale;
            this.transform.localScale = new Vector3(currScale.x, height, currScale.z);
            Vector3 scaleRatios = new Vector3(
                currScale.x / this.transform.localScale.x,
                currScale.y / this.transform.localScale.y,
                currScale.z / this.transform.localScale.z);
            centerParticleSystem();
            SetAllChildrenScales(scaleRatios);
        }
    }
}
