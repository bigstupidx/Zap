using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameCritical
{
    public class RocketZap : Zap
    {
        [SerializeField]
        private int m_NumberOfRowsToJump = 3;

        public override void ApplyEffect()
        {
            base.ApplyEffect();

            GameMaster.Instance.m_PlayerMovement.SetMovementState(PlayerMovement.MovementState.MovingVertical);
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(0.075f, true);
            GameMaster.Instance.m_PlayerMovement.moveVertically(m_NumberOfRowsToJump);
        }
    }
}