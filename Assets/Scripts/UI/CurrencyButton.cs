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

        [SerializeField]
        private float m_ZapModelZOffset = -27.0f;

        /*private void Awake()
        {
            GameObject zapModelPrefab = Resources.Load<GameObject>(PrefabFinder.PREFABS + "Zap_Model_Spinning");
            GameObject go = Instantiate(zapModelPrefab, this.transform.position, Quaternion.identity, this.transform);
            go.transform.localPosition = new Vector3(0, 0, m_ZapModelZOffset);
        }*/

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