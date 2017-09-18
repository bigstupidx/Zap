using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.UI;

namespace GameCritical
{
    public class ReboundZap : Zap
    {
        [SerializeField]
        [Tooltip("Number of hits needed to traverse through this rebound zap")]
        private int m_MaxHits;

        [SerializeField]
        private Color m_HitColor;
        private bool m_HasBeenHit;

        private Color m_ColorIncrementOnHit;

        void Start()
        {
            if (m_ActiveParticleSystem)
            {
                m_ActiveParticleSystem.Stop();
            }

            m_HasBeenHit = false;
            m_MaxHits = Random.Range(1, m_MaxHits + 1);

            m_ColorIncrementOnHit = (m_HitColor - Color) / m_MaxHits;
        }

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
        }

        public override void ApplyCollisionEffect(Collision2D col)
        {
            if(m_HasBeenHit)
            {
                AddAndShowPoints();
                BreakZap(col);
            }
            else
            {
                GameObject go = col.gameObject;
                PlayerStats playerStats = go.GetComponent<PlayerStats>();
                if (playerStats)
                {
                    bool isInvicible = playerStats.GetInvicible();
                    // if the player is not invicible then bounce them back.
                    if (!isInvicible)
                    {
                        // Make the ball bounce back to the original location it was at.
                        GameMaster.Instance.m_PlayerMovement.MoveTo(this, PlayerMovement.MovementState.MovingBounceBackFromZap);
                        m_MaxHits--;
                        m_SpriteRenderer.color += m_ColorIncrementOnHit;
                        if (m_MaxHits <= 0)
                        {
                            // Set has been hit to true so that next time ball hits Zap it will break it
                            m_HasBeenHit = true;
                            // Show the popuptext "REBOUND"
                            ShowPopUptext();
                            if(m_ActiveParticleSystem)
                            {
                                m_ActiveParticleSystem.Play();
                            }
                            // Change the color of the zap to show that it has been hit.
                            if (m_SpriteRenderer)
                            {
                                m_SpriteRenderer.color = m_HitColor;
                            }
                        }
                    }
                    else // if the player is invincible and travels through then give player the points
                    {
                        AddAndShowPoints();
                        m_HasBeenHit = true;
                        // Change the color of the zap to show that it has been hit.
                        if (m_SpriteRenderer)
                        {
                            m_SpriteRenderer.color = m_HitColor;
                        }
                    }
                }
            }
        }
    }
}
