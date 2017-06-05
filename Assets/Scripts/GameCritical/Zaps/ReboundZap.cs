using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameCritical
{
    public class ReboundZap : Zap
    {
        [SerializeField]
        [Tooltip("Number of hits needed to traverse through this rebound zap")]
        private int m_MaxHits;

        [SerializeField]
        private TextMesh m_NumberText;
        private Vector3 m_NumberStartScale;
        [SerializeField]
        private string m_NumberSortingLayer;

        [SerializeField]
        private Color m_HitColor;
        private bool m_HasBeenHit;

        void Start()
        {
            m_ActiveParticleSystem.Stop();
            m_HasBeenHit = false;
            centerObjectOnZap(m_NumberText.gameObject);
            m_MaxHits = Random.Range(1, m_MaxHits + 1);
            m_NumberText.text = m_MaxHits.ToString();
            MeshRenderer renderer = m_NumberText.GetComponent<MeshRenderer>();
            if(renderer)
            {
                renderer.sortingLayerName = m_NumberSortingLayer;
            }
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
                        m_MaxHits--;
                        m_NumberText.text = m_MaxHits.ToString();
                        if (m_MaxHits <= 0)
                        {
                            // Set has been hit to true so that next time ball hits Zap it will break it
                            m_HasBeenHit = true;
                            // Show the popuptext "REBOUND"
                            ShowPopUptext();
                            Destroy(m_NumberText.gameObject);
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
