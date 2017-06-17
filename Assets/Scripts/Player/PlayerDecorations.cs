using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(TrailRenderer))]
    public class PlayerDecorations : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem m_WarpZonePS;
        [SerializeField]
        private ParticleSystem m_TrailPS;
        private TrailRenderer m_TrailRenderer;

        void Awake()
        {
            m_TrailRenderer = GetComponent<TrailRenderer>();
            m_TrailRenderer.sortingLayerName = "Foreground";
            m_TrailRenderer.sortingOrder = 0;
        }

        public void ShowMainPS()
        {
            this.m_WarpZonePS.Stop();
            //this.m_TrailRenderer.enabled = true;
            this.m_TrailPS.Play();
        }

        public void ShowWarpZonePS()
        {
            this.m_WarpZonePS.Play();
            //this.m_TrailRenderer.enabled = false;
            this.m_TrailPS.Stop();
        }

        public ParticleSystem GetWarpZonePS() { return m_WarpZonePS; }
        public ParticleSystem GetTrailPS() { return m_TrailPS; }
        public TrailRenderer GetTrailRenderer() { return m_TrailRenderer; }
    }
}
