using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(TrailRenderer))]
    public class PlayerDecorations : MonoBehaviour
    {
        [SerializeField]
        private string m_PSSortingLayer;
        [SerializeField]
        private ParticleSystem m_WarpZonePS;
        [SerializeField]
        private ParticleSystem m_StartingTrailPS;
        private ParticleSystem m_CurrTrailPS;
        private TrailRenderer m_TrailRenderer;
        private SpriteRenderer m_SpriteRenderer;

        void Awake()
        {
            m_SpriteRenderer = this.GetComponent<SpriteRenderer>();

            // set trail renderer to foreground layer
            m_TrailRenderer = GetComponent<TrailRenderer>();
            m_TrailRenderer.sortingLayerName = m_PSSortingLayer;
            m_TrailRenderer.sortingOrder = 0;

            // set warp zone particle system to foreground layer
            ParticleSystemRenderer psr = m_WarpZonePS.GetComponent<ParticleSystemRenderer>();
            psr.sortingLayerName = m_PSSortingLayer;
            psr.sortingOrder = -1;

            SetTrailPS(m_StartingTrailPS);
        }

        public void SetCharacter(Sprite sprite, Color color)
        {
            m_SpriteRenderer.sprite = sprite;
            m_SpriteRenderer.color = color;
        }

        public void SetTrailPS(ParticleSystem newTrailPSPrefab)
        {
            if(m_CurrTrailPS != null)
            {
                Destroy(m_CurrTrailPS.gameObject);
            }
            m_CurrTrailPS = Instantiate(newTrailPSPrefab, this.transform);
            m_CurrTrailPS.transform.localPosition = Vector3.zero;
            ParticleSystemRenderer psr = m_CurrTrailPS.GetComponent<ParticleSystemRenderer>();
            psr.sortingLayerName = m_PSSortingLayer;
            psr.sortingOrder = -1;
        }

        public void ShowMainPS()
        {
            this.m_WarpZonePS.Stop();
            this.m_WarpZonePS.gameObject.SetActive(false);
            //this.m_TrailRenderer.enabled = true;
            this.m_CurrTrailPS.Play();
        }

        public void ShowWarpZonePS()
        {
            this.m_WarpZonePS.gameObject.SetActive(true);
            this.m_WarpZonePS.Play();
            //this.m_TrailRenderer.enabled = false;
            this.m_CurrTrailPS.Stop();
        }

        public void HideAll()
        {
            m_CurrTrailPS.Stop();
            m_TrailRenderer.enabled = false;
        }

        public ParticleSystem GetWarpZonePS() { return m_WarpZonePS; }
        public ParticleSystem GetTrailPS() { return m_CurrTrailPS; }
        public TrailRenderer GetTrailRenderer() { return m_TrailRenderer; }
    }
}
