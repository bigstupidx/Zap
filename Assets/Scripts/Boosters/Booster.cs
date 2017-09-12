using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour
    {
        public float m_Duration = 8.0f;
        public float m_CoolDownTime = 5.0f;

        public Sprite m_UISprite;

        public virtual void Activate()
        {
            StartCoroutine(activeTimer());
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