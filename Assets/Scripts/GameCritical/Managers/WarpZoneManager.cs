using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

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

        public void ExitWarpZone()
        {
            GameMaster.Instance.m_ZapManager.SpawnNextZapGrid();
            m_WarpZone.SetWarpParticleSystem(false);
            SetInputEnabled(false);
            GameMaster.Instance.m_CameraFollow.ResetOffset(m_CamOffsetResetTime);
            StartCoroutine(GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel.SlideOut());
        }

        void Update()
        {
            if (m_IsInputEnabled)
            {
                // PC INPUT
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ExitWarpZone();
                }
            }
        }

        public void SetInputEnabled(bool isInputEnabled)
        {
            m_IsInputEnabled = isInputEnabled;
        }

        public WarpZone SpawnDeadZone()
        {
            Vector3 topMiddleInWorldSpace = ScreenUtilities.GetWSofSSPosition(0.5f, 1.0f);
            topMiddleInWorldSpace = new Vector3(topMiddleInWorldSpace.x, topMiddleInWorldSpace.y, GameMaster.Instance.m_PlayerMovement.transform.position.z);
            m_WarpZone = (WarpZone)Instantiate(m_WarpZonePrefab, topMiddleInWorldSpace + m_WarpZonePrefab.GetOriginOffsetPosition(), Quaternion.identity);
            return m_WarpZone;
        }

        public WarpZone GetDeadZone()
        {
            return m_WarpZone;
        }
    }
}
