using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameCritical
{
    public class RocketZap : Zap
    {
        [SerializeField]
        private int m_NumberOfRowsToJump = 50;

        [SerializeField]
        private float m_SpeedMultiplier = 0.075f;

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();

            GameMaster.Instance.m_PlayerMovement.SetMovementState(PlayerMovement.MovementState.MovingVertical);
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(m_SpeedMultiplier, false);
            GameMaster.Instance.m_PlayerMovement.MoveVertically(m_NumberOfRowsToJump);
        }

        public override void ApplyCollisionEffect()
        {
            base.ApplyCollisionEffect();
        }
    }
}