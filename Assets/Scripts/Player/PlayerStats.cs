using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem m_DeathParticleSystem;
        [SerializeField]
        private float m_GameRestartDelay = 1.8f;
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
            StartCoroutine(EndGameAfterDeathAnimation());
            m_PlayerMovement.enabled = false;
            m_SpriteRenderer.enabled = false;
        }

        IEnumerator EndGameAfterDeathAnimation()
        {
            yield return new WaitForSeconds(m_GameRestartDelay);
            GameCritical.GameMaster.Instance.m_UIManager.m_MainMenuPanel.Show();
            Destroy(this.gameObject);
        }
    }
}
