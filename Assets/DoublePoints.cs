using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Boosters
{
    public class DoublePoints : Booster
    {
        public override void Activate()
        {
            base.Activate();
            Zap.m_PointMultiplier = 2;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Zap.m_PointMultiplier = 1;
        }
    }
}
