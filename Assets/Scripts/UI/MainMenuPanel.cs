﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameCritical;

namespace UI
{
    public class MainMenuPanel : UIPanel
    {
        public void PlayGame()
        {
            GameCritical.GameMaster.Instance.PlayGame();
            this.Hide();
        }
    }
}