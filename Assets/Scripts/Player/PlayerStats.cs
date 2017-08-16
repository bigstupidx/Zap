using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem m_DeathParticleSystem;
        private bool m_Invincible = false;

        private PlayerMovement m_PlayerMovement;
        private SpriteRenderer m_SpriteRenderer;

        void Start()
        {
            m_Invincible = false;
            m_PlayerMovement = this.GetComponent<PlayerMovement>();
            m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public bool GetInvicible()
        {
            return m_Invincible;
        }

        public void SetInvicible(bool invincible)
        {
            m_Invincible = invincible;
        }

        public void Kill()
        {
            Instantiate(m_DeathParticleSystem, this.transform.position, Quaternion.identity);
            m_PlayerMovement.enabled = false;
            m_SpriteRenderer.enabled = false;
            Destroy(this.gameObject);

            PlayerDecorations pd = GetComponent<PlayerDecorations>();
            if (pd)
            {
                pd.HideAll();
            }

            GameCritical.GameMaster.Instance.EndGame();
        }
    }
}
