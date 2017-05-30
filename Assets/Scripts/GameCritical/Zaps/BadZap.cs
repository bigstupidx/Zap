﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameCritical
{
    public class BadZap : Zap
    {
        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
        }

        public override void ApplyCollisionEffect(GameObject go)
        {
            base.ApplyCollisionEffect(go);
            PlayerStats playerStats = go.GetComponent<PlayerStats>();
            if(playerStats)
            {
                bool isInvicible = playerStats.GetInvicible();
                if(!isInvicible)
                {
                    GameMaster.Instance.m_PlayerMovement.MoveTo(this);
                }
            }
        }
    }
}
