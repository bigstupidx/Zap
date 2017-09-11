using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour
    {
        public float m_Duration = 8.0f;

        [SerializeField]
        protected Sprite m_UISprite;

        public virtual void Activate()
        {
            StartCoroutine(activeTimer());
        }

        public virtual void Deactivate()
        {

        }

        private IEnumerator activeTimer()
        {
            yield return new WaitForSeconds(m_Duration);
            Deactivate();
        }
    }
}