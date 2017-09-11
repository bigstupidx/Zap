using UnityEngine;
using GameCritical;
using System.Collections;
using Utility;

namespace Boosters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AutoTurretBullet : MonoBehaviour
    {
        [SerializeField]
        private float m_ZapTransitionTime = 0.5f;

        private Zap m_TargetZap;
        private float m_BulletLerpTime = 0;

        private GoodZap goodZap;

        private void Start()
        {
            goodZap = Resources.Load<GoodZap>(PrefabFinder.ZAPS + "GoodZap");
        }

        public void SetTarget(Zap zap, float bulletLerpTime)
        {
            if (zap != null)
            {
                m_BulletLerpTime = bulletLerpTime;
                m_TargetZap = zap;
                StopCoroutine(lerpToTarget());
                StartCoroutine(lerpToTarget());
            }
        }

        private IEnumerator lerpToTarget()
        {
            float currTime = 0;
            Vector3 startPos = this.transform.position;

            while(currTime < m_BulletLerpTime)
            {
                currTime += Time.deltaTime;
                float percentageCompleted = currTime / m_BulletLerpTime;
                this.transform.position = Vector3.Lerp(
                    startPos, 
                    m_TargetZap.transform.position + new Vector3(m_TargetZap.Width / 2.0f, -m_TargetZap.Height / 2.0f, 0), 
                    percentageCompleted
                    );
                yield return null;
            }

            // spawn green zap in place of previous zap
            GoodZap instance = Instantiate(goodZap, m_TargetZap.transform.position, m_TargetZap.transform.rotation);
            ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
            StartCoroutine(currZapGrid.DestroyAndReplaceZap(m_TargetZap.Row, m_TargetZap.Col, instance, m_ZapTransitionTime, this));
            //Destroy(this.gameObject);
        }

    }
}
