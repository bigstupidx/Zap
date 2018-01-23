using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class MainMenuPanel : UIPanel
    {
        public Button m_LogoutButton;
        public Button m_LoginButton;

        public void PlayGame()
        {
            GameCritical.GameMaster.Instance.PlayGame();
            this.Hide();
        }

        public void Rate()
        {
            Debug.Log("Pressed Rate Button");
        }

        public void ShowLeaderboards()
        {
            Debug.Log("Pressed Leaderboard Button");
        }

        public void ShowConfirmLogout()
        {
            GameMaster.Instance.m_UIManager.m_ConfirmLogoutPanel.Show();
        }
    }
}