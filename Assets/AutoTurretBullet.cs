using UnityEngine;
using GameCritical;
using System.Collections;

namespace Boosters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AutoTurretBullet : MonoBehaviour
    {
        private Zap m_TargetZap;
        private float m_BulletLerpTime = 0;

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
            GoodZap goodZap = Resources.Load<GoodZap>(PrefabFinder.ZAPS + "GoodZap");
            GoodZap instance = Instantiate(goodZap, m_TargetZap.transform.position, m_TargetZap.transform.rotation);
            ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
            currZapGrid.DestroyAndReplaceZap(m_TargetZap.Row, m_TargetZap.Col, instance);
            Destroy(this.gameObject);
        }

    }
}
