using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAP;
using UnityEngine.UI;

namespace UI
{
    public class WarpStorePanel : UIPanel
    {
        public HorizontalLayoutGroup m_ContentPanel;
        public List<WarpStoreButton> m_MainMenuOptions;
        public List<WarpStoreButton> m_CurrencyMenuOptions;
        public List<WarpStoreButton> m_BoostMenuOptions;
        public List<WarpStoreButton> m_CustomizationMenuOptions;

        private void Start()
        {
            OpenMainMenuPanel();
        }

        public void Buy100Zaps()
        {
            IAPManager.Instance.Buy100Gold();  
        }

        public void Buy50Zaps()
        {
            IAPManager.Instance.Buy50Gold();
        }

        public void OpenMainMenuPanel()
        {
            Debug.Log("Open main menu panel");
            PopulateOptions(m_MainMenuOptions);
        }

        public void OpenCurrencyPanel()
        {
            Debug.Log("Open currency panel");
            PopulateOptions(m_CurrencyMenuOptions);
        }

        public void OpenCustomizePanel()
        {
            Debug.Log("Open customize panel");
            PopulateOptions(m_CustomizationMenuOptions);
        }

        public void OpenBoostsPanel()
        {
            Debug.Log("Open boosts panel");
            PopulateOptions(m_BoostMenuOptions);
        }

        public void PopulateOptions(List<WarpStoreButton> options)
        {
            int children = m_ContentPanel.transform.childCount;
            for(int i = children - 1; i >= 0; i--)
            {
                Destroy(m_ContentPanel.transform.GetChild(i).gameObject);
            }

            int newChildren = options.Count;
            for (int i = 0; i < newChildren; i++)
            {
                Instantiate(options[i], m_ContentPanel.transform);
            }
        }
    }
}
