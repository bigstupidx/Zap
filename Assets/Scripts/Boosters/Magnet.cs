using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Boosters
{
    public class Magnet : Booster
    {
        [SerializeField]
        private float m_LerpTime = 2.0f;

        private List<Zap> m_ZapsAlreadyBeingLerped;

        private List<Zap> m_LucrativeZaps;

        private void Awake()
        {
            m_ZapsAlreadyBeingLerped = new List<Zap>();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        void Update()
        {
            if (m_LucrativeZaps == null)
            {
                ZapGrid zapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
                m_LucrativeZaps = zapGrid.GetLucrativeZaps();
            }
            else
            {
                for (int i = 0; i < m_LucrativeZaps.Count; i++)
                {
                    Zap currZap = m_LucrativeZaps[i];
                    if(currZap != null)
                    {
                        if (currZap.transform.position.y < this.transform.position.y && !m_ZapsAlreadyBeingLerped.Contains(currZap))
                        {
                            // lerp to magnet
                            currZap.DisableCollision();
                            m_ZapsAlreadyBeingLerped.Add(currZap);
                            StartCoroutine(lerpZapToThisMagnet(currZap));
                        }
                    }
                }
            }
        }

        private IEnumerator lerpZapToThisMagnet(Zap zap)
        {
            float currTime = 0;
            zap.AddAndShowPoints();
            while (currTime < m_LerpTime)
            {
                currTime += Time.deltaTime;
                zap.transform.position = Vector3.Lerp(zap.transform.position, this.transform.position, currTime / m_LerpTime);
                zap.transform.localScale = Vector3.Lerp(zap.transform.localScale, Vector3.zero, currTime / m_LerpTime);
                yield return null;
            }

            m_LucrativeZaps.Remove(zap);
            m_ZapsAlreadyBeingLerped.Remove(zap);
            Destroy(zap.gameObject);
        }
    }
}
