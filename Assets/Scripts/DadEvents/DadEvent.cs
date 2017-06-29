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

        public virtual void Play() { }

        public virtual void Stop()
        {
            DadEventManager dadEventManger = GameMaster.Instance.m_DadEventManager;
            if(dadEventManger != null)
            {
                dadEventManger.RemoveEvent(this);
            }
        }
    }
}
