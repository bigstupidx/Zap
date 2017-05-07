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
        }

        public override void ApplyCollisionEffect()
        {
            base.ApplyCollisionEffect();
            GameMaster.Instance.m_PlayerMovement.InterruptAndMoveTo(this);
        }
    }
}
