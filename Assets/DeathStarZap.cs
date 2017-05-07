using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class DeathStarZap : Zap
    {
        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            GameMaster.Instance.m_DeathStar.SetSpeedMultiplier(2.0f);
        }

        public override void ApplyCollisionEffect()
        {
            base.ApplyCollisionEffect();
        }
    }
}
