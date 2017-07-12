using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace DadEvents
{
    public class DadEvent : MonoBehaviour
    {
        [SerializeField]
        private string m_EventName;
        [SerializeField]
        private float m_Duration = 10.0f;

        private void Awake()
        {
            StartCoroutine(durationTimer());
        }

        public virtual void Play() { }

        public virtual void Stop()
        {
            DadEventManager dadEventManger = GameMaster.Instance.m_DadEventManager;
            if(dadEventManger != null)
            {
                dadEventManger.RemoveEvent(this);
            }
        }

        private IEnumerator durationTimer()
        {
            yield return new WaitForSeconds(m_Duration);
            Stop();
        }
    }
}
