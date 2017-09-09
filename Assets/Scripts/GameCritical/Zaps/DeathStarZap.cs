using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class DeathStarZap : Zap
    {
        [SerializeField]
        [Tooltip("Multiplier that changes how fast the death star moves")]
        private float m_SpeedMultiplier;

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            GameMaster.Instance.m_DeathStar.SetSpeedMultiplier(m_SpeedMultiplier, true);
        }

        public override void ApplyCollisionEffect(Collision2D col)
        {
            base.ApplyCollisionEffect(col);
            GameObject go = col.gameObject;
        }
    }
}
