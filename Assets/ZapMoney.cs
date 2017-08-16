using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

[RequireComponent(typeof(CircleCollider2D))]
public class ZapMoney : MonoBehaviour {

    [SerializeField]
    private float m_RotateSpeed = 2.0f;
    [SerializeField]
    private float m_RandomRotationMax = 0.2f;
    [SerializeField]
    private float m_CollectSpeed = 4.0f;
    [SerializeField]
    private ParticleSystem m_ZapExplodePS;

    private CircleCollider2D m_CircleCollider2D;
    private StatsManager m_StatsManager;
    private DeathStar m_DeathStar;

    void Awake()
    {
        m_CircleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        m_StatsManager = GameMaster.Instance.m_StatsManager;
        if (m_StatsManager == null)
        {
            Debug.LogError("Cant find stats manager in ZapMoney");
        }

        m_DeathStar = GameMaster.Instance.m_DeathStar;
        if (m_DeathStar == null)
        {
            Debug.LogError("Cant find death star in ZapMoney");
        }
    }

    void Update()
    {
        float randomRotateX = Random.Range(-m_RandomRotationMax, m_RandomRotationMax);
        float randomRotateZ = Random.Range(-m_RandomRotationMax, m_RandomRotationMax);
        this.transform.Rotate(new Vector3(randomRotateX, 1, randomRotateZ) * m_RotateSpeed);
        if(m_DeathStar.transform.position.y >= this.transform.position.y)
        {
            if(m_ZapExplodePS != null)
            {
                Instantiate(m_ZapExplodePS, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            m_CircleCollider2D.enabled = false;
            StartCoroutine(MoveToZapUICounter());
        }
    }

    private IEnumerator MoveToZapUICounter()
    {
        // lerp zap to zap banker
        Vector3 targetPos = GameMaster.Instance.m_UIManager.m_InfoPanel.m_ZapBanker.GetImagePosition();
        Vector3 startPos = this.transform.position;
        Vector3 startScale = this.transform.localScale;
        float lerpPercentage = 0.0f;
        while(lerpPercentage < 1.0f)
        {
            lerpPercentage += m_CollectSpeed * Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, targetPos, lerpPercentage);
            this.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, lerpPercentage);
            yield return null;
        }

        // add zap to zaps collected.
        m_StatsManager.AddZaps(1);
        Destroy(this.gameObject);
    }
}
