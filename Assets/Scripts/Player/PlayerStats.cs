using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem m_DeathParticleSystem;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Kill()
        {
            Instantiate(m_DeathParticleSystem, this.transform.position, Quaternion.identity);
            GameCritical.GameMaster.Instance.m_UIManager.m_MainMenuPanel.Show();
            Destroy(this.gameObject);
        }
    }
}
