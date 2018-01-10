﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class GoodZap : Zap
    {
        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            m_StreakManager.IncrementStreakCount();
        }

        public override void ApplyCollisionEffect(Collision2D col)
        {
            base.ApplyCollisionEffect(col);
            BreakZap(col);
        }
    }
}
