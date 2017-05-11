using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class BadZap : Zap
    {
        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(1.0f, false);
        }

        public override void ApplyCollisionEffect()
        {
            base.ApplyCollisionEffect();
            GameMaster.Instance.m_PlayerMovement.MoveTo(this);
        }
    }
}
