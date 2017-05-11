using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField]
        private string m_GameSceneStr;

        public void PlayGame()
        {
            SceneManager.LoadScene(m_GameSceneStr);
        }
    }
}