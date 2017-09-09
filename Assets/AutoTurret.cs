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

        private void Start()
        {
            Activate();
        }

        public override void Activate()
        {
            StartCoroutine(startShootTimer());
        }

        public override void Deactivate()
        {
            StopCoroutine(startShootTimer());
        }

        private IEnumerator startShootTimer()
        {
            yield return new WaitForSeconds(m_ShootInterval);
            // find target to shoot and shoot
            List<Zap> dangerousZaps = GameMaster.Instance.m_ZapManager.GetZapGrid().GetDangerousZaps();
            Vector3 worldSpaceTopScreen = Utility.ScreenUtilities.GetWSofSSPosition(0.0f, 1.0f);

            for (int i = 0; i < dangerousZaps.Count; i++)
            {
                Zap currZap = dangerousZaps[i];
                if(currZap.transform.position.y <= worldSpaceTopScreen.y && !zapsAlreadyShotAt.Contains(currZap))
                {
                    Fire(currZap);
                    zapsAlreadyShotAt.Add(currZap);
                    StartCoroutine(startShootTimer());
                    break;
                }
            }
        }

        private void Fire(Zap targetZap)
        {
            AutoTurretBullet bullet = Instantiate(m_BulletPrefab, m_BulletSpawn.position, m_BulletSpawn.rotation);
            bullet.SetTarget(targetZap, m_BulletLerpTime);
        }
    }
}
