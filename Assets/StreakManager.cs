using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class StreakManager : MonoBehaviour
    {
        private int _streakNum;

        [SerializeField]
        private int _numUntilStreak = 8;

        private bool _inStreakMode;

        private void Awake()
        {
            _inStreakMode = false;
            _streakNum = 0;
        }

        public void IncrementStreakCount()
        {
            _streakNum++;
            if(!_inStreakMode && _streakNum >= _numUntilStreak)
            {
                EnterStreakMode();
                _inStreakMode = true;
            }
        }

        public void ExitStreakMode()
        {
            if(_inStreakMode)
            {
                _streakNum = 0;
                GameMaster.Instance.m_BackDropManager.ShowCurrentStageColors();
                GameMaster.Instance.m_PlayerMovement.DisableStreakSpeed();
                GameMaster.Instance.m_ZapManager.HideDoublePointsOnAllZaps();
                Zap.m_PointMultiplier = 1;
                GameMaster.Instance.m_UIManager.SpawnUINotification("STREAK ENDED", false);
                _inStreakMode = false;
            }
        }

        private void EnterStreakMode()
        {
            GameMaster.Instance.m_BackDropManager.ShowStreakColors();
            GameMaster.Instance.m_PlayerMovement.EnableStreakSpeed();
            Handheld.Vibrate();
            GameMaster.Instance.m_ZapManager.ShowDoublePointsOnAllZaps();
            Zap.m_PointMultiplier = 2;
            GameMaster.Instance.m_UIManager.SpawnUINotification("STREAK!", true);
        }
    }
}
