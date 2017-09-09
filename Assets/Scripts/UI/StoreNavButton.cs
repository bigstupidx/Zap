using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class StoreNavButton : WarpStoreButton
    {
        [Tooltip("Action type that defines what will happen when button is pressed")]
        public WarpStorePanel.NavActionType m_ActionType;

        public override void onButtonClick()
        {
            WarpStorePanel warpStorePanel = GameCritical.GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel;
            if(warpStorePanel != null)
            {
                warpStorePanel.TriggerNavAction(m_ActionType);
            }
}
    }
}