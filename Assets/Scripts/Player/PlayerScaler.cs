﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Player
{
    public class PlayerScaler : MonoBehaviour
    {

        [SerializeField]
        private float m_MaxScale = 0.04f;

        private Vector3 m_StartScale;
        private Vector3 m_CurrScale;
        private bool m_IsScaling;
        private float m_TargetScale;

        // Use this for initialization
        void Start()
        {
            m_StartScale = this.transform.localScale;
            m_CurrScale = m_StartScale;
            m_IsScaling = false;
        }

        public void ResetPlayerScaler()
        {
            m_IsScaling = false;
        }

        public void LerpToZapScale(float lerpPercentage)
        {
            // Lerp size of the player to be half the size of the zap width.
            if (!m_IsScaling)
            {
                ZapGrid zapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
                float zapWidth = zapGrid.GetZap(0, 0).Width;
                Sprite s = this.GetComponent<SpriteRenderer>().sprite;
                float unitWidth = s.textureRect.width / s.pixelsPerUnit;
                float unitHeight = s.textureRect.height / s.pixelsPerUnit;
                m_CurrScale = this.transform.localScale;
                m_TargetScale = zapWidth / unitWidth / 2.0f;
                m_IsScaling = true;
            }
            LerpScale(m_TargetScale, lerpPercentage);
        }

        public void LerpToOriginalScale(float lerpPercentage)
        {
            m_TargetScale = m_StartScale.x;
            LerpScale(m_TargetScale, lerpPercentage);
        }

        private void LerpScale(float targetScale, float lerpPercentage)
        {
            // scale player size
            targetScale = Mathf.Clamp(targetScale, 0.0f, m_MaxScale);
            this.transform.localScale = Vector3.Lerp(m_CurrScale, new Vector3(targetScale, targetScale, targetScale), lerpPercentage);

            // scale trailer renderer size
            //m_TrailRenderer.startWidth = Mathf.Lerp(m_TrailRenderer.startWidth, m_TrailRenderer.startWidth * this.transform.localScale.x / m_StartScale.x, m_LerpPercentage);
            //m_TrailRenderer.startWidth = Mathf.Clamp(m_TrailRenderer.startWidth, )
        }
    }
}
