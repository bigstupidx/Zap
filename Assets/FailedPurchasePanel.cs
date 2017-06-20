using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace UI
{
    public class FailedPurchasePanel : UIPanel
    {
        public void GoToCurrencyPanel()
        {   
            GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel.TriggerAction(WarpStorePanel.ActionType.OPEN_CURRENCY_MENU);
        }
    }
}
