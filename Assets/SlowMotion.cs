using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boosters
{
    public class SlowMotion : Booster
    {
        [Range(0.01f, 1.0f)]
        public float m_SlowMotionPercentage;

        public override void Activate()
        {
            StartCoroutine(activeTimer());
            Time.timeScale = m_SlowMotionPercentage;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Time.timeScale = 1.0f;
        }

        private IEnumerator activeTimer()
        {
            yield return new WaitForSeconds(m_Duration * m_SlowMotionPercentage);
            Deactivate();
        }
    }
}
