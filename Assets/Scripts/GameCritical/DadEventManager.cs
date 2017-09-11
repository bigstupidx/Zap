using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DadEvents;

namespace GameCritical
{
    public class DadEventManager : MonoBehaviour
    {
        [SerializeField]
        private List<DadEvent> m_Events;
        public List<DadEvent> Events { get { return m_Events; } }

        private List<DadEvent> m_CurrentEvents;

        [SerializeField]
        private float m_MinimumRandomRange = 15.0f;
        [SerializeField]
        private float m_MaximumRandomRange = 60.0f;

        private bool m_IsEventTimerRunning = false;

        void Awake()
        {
            m_CurrentEvents = new List<DadEvent>();
            if (m_Events == null)
            {
                m_Events = new List<DadEvent>();
            }
        }

        private IEnumerator eventTimer()
        {
            m_IsEventTimerRunning = true;
            float secondsToWait = Random.Range(m_MinimumRandomRange, m_MaximumRandomRange);
            yield return new WaitForSeconds(secondsToWait);
            PlayRandomEvent();
        }

        public void PlayRandomEvent()
        {
            int randIndex = (int) (Random.Range(0, m_Events.Count));
            DadEvent evtPrefab = m_Events [randIndex];
            DadEvent evtInstance = Instantiate(evtPrefab);
            m_CurrentEvents.Add(evtInstance);
            evtInstance.Play();
        }

        public void StartEvents()
        {
            // don't allow timer coroutine to spawn events start more than once
            if(m_IsEventTimerRunning)
            {
                return;
            }

            m_IsEventTimerRunning = true;
            StartCoroutine(eventTimer());
        }

        public void StopEvents()
        {
            // stop event timer
            m_IsEventTimerRunning = false;
            StopCoroutine(eventTimer());

            // stop all currently active events
            for (int i = m_CurrentEvents.Count - 1; i >= 0; i--)
            {
                DadEvent evt = m_CurrentEvents[i];
                evt.Stop();
            }
        }

        public void RemoveEvent(DadEvent evt)
        {
            if(m_CurrentEvents.Contains(evt))
            {
                m_CurrentEvents.Remove(evt);
            }
        }
    }
}
