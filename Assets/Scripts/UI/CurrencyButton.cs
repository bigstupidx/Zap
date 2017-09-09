using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CurrencyButton : WarpStoreButton
    {
        [Tooltip("Action type that defines what will happen when currency purchased is pressed")]
        public WarpStorePanel.CurrencyActionType m_ActionType;

        public override void onButtonClick()
        {
            WarpStorePanel warpStorePanel = GameCritical.GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel;
            if(warpStorePanel != null)
            {
                warpStorePanel.TriggerCurrencyAction(m_ActionType);
            }
}
    }
}