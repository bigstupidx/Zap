using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using Boosters;

namespace UI
{
    public class BoostUnlockButton : UnlockableButton
    {
        [SerializeField]
        private Booster m_BoosterPrefab;

        public override void Init()
        {
            base.Init();

            // make status icon in center
            // m_StatusInstance.SetCenterLocked();
        }

        public override void equip()
        {
            base.equip();

            GameMaster.Instance.m_PlayerBoost.SetEquippedBooster(m_BoosterPrefab);
        }
    }
}