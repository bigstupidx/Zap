using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace DadEvents
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem m_DestroyPSPrefab;
        [SerializeField]
        private ParticleSystem m_TrailPSPrefab;

        private Rigidbody2D m_RigidBody2D;
        private BoxCollider2D m_BoxCollider2D;
        private SpriteRenderer m_SpriteRenderer;

        private void Awake()
        {
            m_RigidBody2D = this.GetComponent<Rigidbody2D>();
            m_BoxCollider2D = this.GetComponent<BoxCollider2D>();
            m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void AddForce(Vector2 force)
        {
            m_RigidBody2D.AddForce(force);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            PlayerStats playerStats = col.GetComponent<Player.PlayerStats>();
            if (playerStats != null)
            {
                if (m_DestroyPSPrefab != null)
                {
                    Instantiate(m_DestroyPSPrefab, this.transform.position, Quaternion.identity);
                }
                playerStats.Kill();
                m_SpriteRenderer.enabled = false;
                m_TrailPSPrefab.Stop();
                this.enabled = false;
            }
        }
    }
}