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

        [SerializeField]
        private GameObject m_UpArrowSprite;
        [SerializeField]
        private Vector3 m_UpArrowFixedScale;
        private GameObject m_UpArrowSpriteInstance;

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
            GameMaster.Instance.m_PlayerStats.SetInvicible(true);
            GameMaster.Instance.m_PlayerMovement.SetMovementState(PlayerMovement.MovementState.MovingRocketJump);
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(m_SpeedMultiplier, false);
            GameMaster.Instance.m_PlayerMovement.MoveRocketJump(m_NumberOfRowsToJump);
        }

        void OnDestroy()
        {
            if(m_UpArrowSpriteInstance)
            {
                Destroy(m_UpArrowSpriteInstance.gameObject);
            }
        }

        public override void ApplyCollisionEffect(GameObject go)
        {
            base.ApplyCollisionEffect(go);
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