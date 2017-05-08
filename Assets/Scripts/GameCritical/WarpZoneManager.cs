using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class WarpZoneManager : MonoBehaviour
    {
        [SerializeField]
        private WarpZone m_WarpZonePrefab;
        private WarpZone m_WarpZone;

        [SerializeField]
        private float m_CamOffsetResetTime;

        private bool m_IsInputEnabled;

        void Awake()
        {
            m_IsInputEnabled = false;
        }

        void Update()
        {
            if (m_IsInputEnabled)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameMaster.Instance.m_ZapManager.SpawnNextZapGrid();
                    m_WarpZone.SetWarpParticleSystem(false);
                    SetInputEnabled(false);
                    GameMaster.Instance.m_CameraFollow.ResetOffset(m_CamOffsetResetTime);
                    GameMaster.Instance.m_UIManager.m_WarpStorePanel.Hide();
                    GameMaster.Instance.m_BackDropManager.ShowNormalColors();
                }
            }
        }

        public void SetInputEnabled(bool isInputEnabled)
        {
            m_IsInputEnabled = isInputEnabled;
        }

        public WarpZone SpawnDeadZone()
        {
            Vector3 spawnPos = GameMaster.Instance.m_ZapManager.GetZapGrid().GetTopMiddle();
            m_WarpZone = (WarpZone)Instantiate(m_WarpZonePrefab, spawnPos + m_WarpZonePrefab.GetOriginOffsetPosition(), Quaternion.identity);
            return m_WarpZone;
        }

        public WarpZone GetDeadZone()
        {
            return m_WarpZone;
        }
    }
}
