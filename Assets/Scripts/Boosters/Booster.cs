using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Boosters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Booster : MonoBehaviour
    {
        public float m_Duration = 8.0f;
        public float m_CoolDownTime = 5.0f;

        public Sprite m_UISprite;

        public bool shouldShowOnPlayer = true;

        public SpriteRenderer spriteRenderer;

        [SerializeField]
        private string notificationText;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void Activate()
        {
            StartCoroutine(activeTimer());
            if(notificationText.Length > 0)
            {
                GameMaster.Instance.m_UIManager.SpawnUINotification(notificationText, true);
            }
        }

        public virtual void Deactivate()
        {
            Destroy(this.gameObject);
        }

        private IEnumerator activeTimer()
        {
            yield return new WaitForSeconds(m_Duration);
            Deactivate();
        }
    }
}