using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class TimerHelper : MonoBehaviour
    {

        public static TimerHelper instance;

        private void Awake()
        {
            instance = this;
        }

        public IEnumerator StartTimer(float time)
        {
            float currTime = 0.0f;
            while (currTime < time)
            {
                currTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
