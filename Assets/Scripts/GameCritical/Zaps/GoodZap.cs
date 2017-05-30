using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class GoodZap : Zap
    {
        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
        }

        public override void ApplyCollisionEffect(GameObject go)
        {
            base.ApplyCollisionEffect(go);
        }
    }
}
