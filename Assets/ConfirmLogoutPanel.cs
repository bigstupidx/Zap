using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace UI
{
    public class ConfirmLogoutPanel : UIPanel
    {
        public void Logout()
        {
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemoveUsername();
            GameMaster.Instance.m_UIManager.m_InfoPanel.RemovePassword();
            GameMaster.Instance.m_UIManager.m_LoginSignupPanels.Show();
            Hide();
        }

        public void Cancel()
        {
            Hide();
        }
    }
}
