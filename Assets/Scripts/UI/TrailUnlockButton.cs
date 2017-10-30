using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace UI
{
    public class TrailUnlockButton : UnlockableButton {

        [SerializeField]
        private ParticleSystem m_TrailPSPrefab;
        private RotatingTrail m_RotatingTrailObject;

        public override void Init()
        {
            base.Init();

            costInZaps = true;

            // instantiate particle system
            m_RotatingTrailObject = this.GetComponentInChildren<RotatingTrail>();
            ParticleSystem ps = Instantiate(m_TrailPSPrefab, m_RotatingTrailObject.transform);
            ps.transform.localPosition = Vector3.zero;

            // make status icon in center
            m_StatusInstance.SetCenterLocked();
        }

        public override void equip()
        {
            base.equip();

            GameMaster.Instance.m_PlayerDecorations.SetTrailPS(m_TrailPSPrefab);
        }
    }
}
