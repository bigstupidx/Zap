using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Boosters
{
    public class AutoTurret : Booster
    {
        [SerializeField]
        private AutoTurretBullet m_BulletPrefab; 

        [SerializeField]
        private Transform m_BulletSpawn;

        [SerializeField]
        private float m_ShootInterval = 0.6f;

        [SerializeField]
        private float m_BulletLerpTime = 0.8f;

        private List<Zap> zapsAlreadyShotAt;

        private void Awake()
        {
            zapsAlreadyShotAt = new List<Zap>();
        }

        public override void Activate()
        {
            base.Activate();

            // center the turret on the player
            this.transform.localPosition = Vector3.zero;

            startShootTimer();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        private void startShootTimer()
        {
            Zap targetZap = null;

            // find target to shoot and shoot
            ZapGrid zapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
            if(zapGrid != null)
            {
                List<Zap> dangerousZaps = zapGrid.GetDangerousZaps();
                Vector3 worldSpaceTopScreen = Utility.ScreenUtilities.GetWSofSSPosition(0.0f, 1.0f);

                for (int i = 0; i < dangerousZaps.Count; i++)
                {
                    Zap currZap = dangerousZaps[i];
                    if (currZap.transform.position.y <= worldSpaceTopScreen.y && 
                        currZap.transform.position.y > this.transform.position.y &&
                        !zapsAlreadyShotAt.Contains(currZap))
                    {
                        targetZap = currZap;
                        break;
                    }
                }
            }

            if(targetZap != null)
            {
                StartCoroutine(rotateToTargetThenFire(targetZap));
            }
            else
            {
                Invoke("startShootTimer", m_ShootInterval);
            }
        }

        private IEnumerator rotateToTargetThenFire(Zap targetZap)
        {
            float currTime = 0;
            Quaternion startRotation = this.transform.rotation;
            while(currTime < m_ShootInterval)
            {
                currTime += Time.deltaTime;
                Vector3 vectorToTarget = targetZap.GetCenter() - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, currTime/ m_ShootInterval);
                yield return null;
            }

            zapsAlreadyShotAt.Add(targetZap);
            Fire(targetZap);
            startShootTimer();
        }

        private void Fire(Zap targetZap)
        {
            AutoTurretBullet bullet = Instantiate(m_BulletPrefab, m_BulletSpawn.position, m_BulletSpawn.rotation);
            bullet.SetTarget(targetZap, m_BulletLerpTime);
        }
    }
}
