using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameCritical
{
    public class ReboundZap : Zap
    {
        [SerializeField]
        private Color m_HitColor;
        private bool m_HasBeenHit;

        void Start()
        {
            m_ActiveParticleSystem.Stop();
            m_HasBeenHit = false;
        }

        public override void ApplyImmediateEffect()
        {
            base.ApplyImmediateEffect();
        }

        public override void ApplyCollisionEffect(GameObject go)
        {
            if(m_HasBeenHit)
            {
                AddAndShowPoints();
            }
            else
            {
                m_ActiveParticleSystem.Play();
                PlayerStats playerStats = go.GetComponent<PlayerStats>();
                if (playerStats)
                {
                    bool isInvicible = playerStats.GetInvicible();
                    // if the player is not invicible then bounce them back.
                    if (!isInvicible)
                    {
                        // Make the ball bounce back to the original location it was at.
                        GameMaster.Instance.m_PlayerMovement.MoveTo(this);
                        // Set has been hit to true so that next time ball hits Zap it will break it
                        m_HasBeenHit = true;
                        // Show the popuptext "REBOUND"
                        ShowPopUptext();
                        // Change the color of the zap to show that it has been hit.
                        if (m_SpriteRenderer)
                        {
                            m_SpriteRenderer.color = m_HitColor;
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
