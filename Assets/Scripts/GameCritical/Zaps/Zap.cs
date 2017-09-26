using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
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
        public Color Color
        {
            get
            {
                return m_Color;
            }
        }

        [System.Serializable]
        public class BreakSettings
        {
            public float m_BreakPower = 1.0f;
            public float m_LowerX = 600.0f;
            public float m_UpperX = 800.0f;
            public float m_LowerY = 1620.0f;
            public float m_UpperY = 1700.0f;
            public float m_LowerTorque = 20.0f;
            public float m_UpperTorque = 40.0f;
            public float m_FadeTime = 1.0f;
        }
        [SerializeField]
        private BreakSettings m_BS;

        [SerializeField]
        [Tooltip("If true this zap will be targeted by auto turret booster")]
        private bool m_IsDangerousZap = false;
        public bool IsDangerousZap { get { return m_IsDangerousZap; } set { m_IsDangerousZap = value; } }

        public float Width { get { return m_SpriteRenderer.bounds.size.x; } }
        public float Height { get { return m_SpriteRenderer.bounds.size.y; } }
        private Vector3 m_OffsetPosition;
        private int m_Row;
        public int Row { get { return m_Row; } set { m_Row = value; } }
        private int m_Col;
        public int Col { get { return m_Col; } set { m_Col = value; } }

        public SpriteRenderer m_SpriteRenderer;

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

        public Vector3 GetCenter()
        {
            return this.transform.position + new Vector3(this.Width / 2.0f, this.Height / 2.0f, 0);
        }

        public virtual void ApplyImmediateEffect() { }

        public virtual void ApplyCollisionEffect(Collision2D col)
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

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                ApplyCollisionEffect(col);
            }
        }

        protected void BreakZap(Collision2D col)
        {
            if(col.contacts != null && col.contacts.Length > 0)
            {
                Vector3 contactPoint = new Vector3(col.contacts[0].point.x, this.transform.position.y, this.transform.position.z);
                Vector3 zapPositionBotLeft = this.transform.position;
                Vector3 zapPositionBotRight = this.transform.position + new Vector3(this.Width, 0, 0);

                // get new widths of zaps to spawn
                float widthFirstSection = Vector3.Distance(zapPositionBotLeft, contactPoint);
                float widthSecondSection = Vector3.Distance(contactPoint, zapPositionBotRight);
                float entireWidth = widthFirstSection + widthSecondSection;
                float percentageFirstSection = widthFirstSection / entireWidth;
                float percentageSecondSection = widthSecondSection / entireWidth;


                // Get width of a zap
                ZapGrid zapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
                if(zapGrid != null)
                {
                    float zapWidth = zapGrid.GetZapWidth();

                    // Instantiate splitting zaps
                    Zap zap1 = Instantiate(this, this.transform.position, Quaternion.identity);
                    zap1.SetWidth(zapWidth * percentageFirstSection);
                    zap1.ApplyForce(false);
                    zap1.DisableCollision();
                    zap1.FadeThenDestroy();

                    Zap zap2 = Instantiate(this, contactPoint, Quaternion.identity);
                    zap2.SetWidth(zapWidth * percentageSecondSection);
                    zap2.ApplyForce(true);
                    zap2.DisableCollision();
                    zap2.FadeThenDestroy();

                    //Destroy(this.gameObject);
                }

            }
        }

        public void FadeThenDestroy()
        {
            StartCoroutine(IFadeThenDestroy());
        }
        private IEnumerator IFadeThenDestroy()
        {
            float elapsedTime = 0.0f;
            Color startColor = m_SpriteRenderer.color;
            Color endColor = startColor;
            endColor.a = 0.0f;
            while (elapsedTime < m_BS.m_FadeTime)
            {
                m_SpriteRenderer.color = Color.Lerp(startColor, endColor, elapsedTime / m_BS.m_FadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Destroy(this.gameObject);
        }

        private void ApplyForce(bool ultimatelyMoveRight)
        {
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.gravityScale = 0.0f;
            
            if (ultimatelyMoveRight)
            {
                rigidbody2D.AddForce(new Vector2(Random.Range(m_BS.m_LowerX, m_BS.m_UpperX) * m_BS.m_BreakPower, 
                    Random.Range(m_BS.m_LowerY, m_BS.m_UpperY) * m_BS.m_BreakPower));
                rigidbody2D.AddTorque(Random.Range(-m_BS.m_LowerTorque, -m_BS.m_UpperTorque) * m_BS.m_BreakPower);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(Random.Range(-m_BS.m_LowerX, -m_BS.m_UpperX) * m_BS.m_BreakPower,
                    Random.Range(m_BS.m_LowerY, m_BS.m_UpperY) * m_BS.m_BreakPower));
                rigidbody2D.AddTorque(Random.Range(m_BS.m_LowerTorque, m_BS.m_UpperTorque) * m_BS.m_BreakPower);
            }
        }

        public void DisableCollision()
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            if(boxCollider2D != null)
            {
                boxCollider2D.enabled = false;
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
