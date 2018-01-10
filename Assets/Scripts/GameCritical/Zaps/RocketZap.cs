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

        [SerializeField]
        private float m_SpeedMultiplier = 0.075f;

        [SerializeField]
        private GameObject m_UpArrowSprite;
        [SerializeField]
        private Vector3 m_UpArrowFixedScale;
        private GameObject m_UpArrowSpriteInstance;

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            GameMaster.Instance.m_PlayerStats.SetInvicible(true);
            PlayerMovement playerMovement = GameMaster.Instance.m_PlayerMovement;
            playerMovement.SetMovementState(PlayerMovement.MovementState.MovingRocketJump);
            playerMovement.SetSpeedMultiplier(m_SpeedMultiplier, true);
            playerMovement.MoveRocketJump(m_NumberOfRowsToJump);
            m_StreakManager.IncrementStreakCount();

            // play lockdown animation 
            PlayerDecorations playerDecorations = GameMaster.Instance.m_PlayerDecorations;
            if (playerDecorations != null)
            {
                playerDecorations.ActivateLockdown();
                playerDecorations.ActivateAtmosphereFire();
            }
        }

        void OnDestroy()
        {
            if(m_UpArrowSpriteInstance)
            {
                Destroy(m_UpArrowSpriteInstance.gameObject);
            }
        }

        public override void ApplyCollisionEffect(Collision2D col)
        {
            base.ApplyCollisionEffect(col);
            BreakZap(col);
        }

        public override void SetWidth(float width)
        {
            base.SetWidth(width);
            SpawnUpArrowSprite();
        }

        public override void SetHeight(float height)
        {
            base.SetHeight(height);
            SpawnUpArrowSprite();
        }

        private void SpawnUpArrowSprite()
        {
            if(m_UpArrowSprite)
            {
                if (m_UpArrowSpriteInstance == null)
                {
                    m_UpArrowSpriteInstance = Instantiate(m_UpArrowSprite, this.transform);
                    m_UpArrowSpriteInstance.transform.position = this.transform.position + new Vector3(Width / 2.0f, Height / 2.0f, 0);
                }
                m_UpArrowSpriteInstance.transform.localScale = m_UpArrowFixedScale;
            }
        }
    }
}