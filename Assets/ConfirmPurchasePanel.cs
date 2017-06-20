using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ConfirmPurchasePanel : UIPanel
    {
        [SerializeField]
        private Text m_NameText;
        private UnityAction m_ConfirmFunc;

        public void Populate(UnityAction confirmFunc, string itemName)
        {
            m_ConfirmFunc = confirmFunc;
            m_NameText.text = itemName;
            Show();
        }

        public void Confirm()
        {
            m_ConfirmFunc.Invoke();
        }
    }
}
