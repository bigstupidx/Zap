using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WarpPanel : UIPanel
    {
        [SerializeField]
        private List<WarpStoreButton> m_Options;

        [SerializeField]
        private WarpStorePanel.ActionType m_PrevPanelActionType = WarpStorePanel.ActionType.OPEN_MAIN_MENU;

        private RectTransform m_RectTransform;

        void Awake()
        {
            m_RectTransform = this.GetComponent<RectTransform>();
        }

        public override void Show()
        {
            base.Show();
            PopulateChildren(m_Options);
        }

        public void PopulateChildren(List<WarpStoreButton> newChildren)
        {
            int children = this.transform.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }

            int numChildren = newChildren.Count;
            for (int i = 0; i < numChildren; i++)
            {
                Instantiate(newChildren[i], this.transform);
            }
        }

        public WarpStorePanel.ActionType GetPreviousPanelActionType()
        {
            return m_PrevPanelActionType;
        }

        public RectTransform GetRectTransform()
        {
            if(m_RectTransform == null)
            {
                m_RectTransform = this.GetComponent<RectTransform>();
            }
            return m_RectTransform;
        }
    }
}
