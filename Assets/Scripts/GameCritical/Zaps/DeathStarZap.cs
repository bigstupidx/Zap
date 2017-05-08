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
            GameMaster.Instance.m_DeathStar.SetSpeedMultiplier(1.5f, true);
        }

        public override void ApplyCollisionEffect()
        {
            base.ApplyCollisionEffect();
            GameMaster.Instance.m_UIManager.SpawnPopUpText(m_PopUpText, 
                this.transform.position + new Vector3(Width / 2.0f, 0, 0),
                Color.black);
        }
    }
}
