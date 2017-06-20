using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using Utlities;

namespace GameCritical
{
    public class WarpZone : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Origin position of dead zone is the offset from the top of the last zap grid and is where the player moves to")]
        private Vector3 m_OriginPosition;
        [SerializeField]
        [Tooltip("Camera offset as a percentage from bottom of screen when in dead zone")]
        [Range(0.0f, 1.0f)]
        private float m_CamOffsetAsPercentage;
        [SerializeField]
        [Tooltip("How long it takes to reach cam offset")]
        private float m_LerpTimeToCamOffset = 2.0f;

        void Start()
        {
            // calculate camera offset by a percentage. So if offset is 0.8f then ball will be 80% up the screen.
            Vector3 playerTargetLocationWS = ScreenUtilities.GetWSofSSPosition(0.5f, m_CamOffsetAsPercentage);
            Vector3 cameraTargetLocationWS = ScreenUtilities.GetWSofSSPosition(0.5f, 0.5f);
            Vector3 camOffset = playerTargetLocationWS - cameraTargetLocationWS;
            camOffset = -camOffset;
            GameMaster.Instance.m_CameraFollow.SetOffset(camOffset, m_LerpTimeToCamOffset);

            DeathStar deathStar = GameMaster.Instance.m_DeathStar;
            if(deathStar)
            {
                deathStar.SetIsMoving(false);
            }
            SetWarpParticleSystem(true);
        }

        public void SetWarpParticleSystem(bool isOn)
        {
            GameMaster.Instance.m_WarpParticleSystem.gameObject.SetActive(isOn);
        }

        public Vector3 GetOriginOffsetPosition()
        {
            return m_OriginPosition;
        }
    }
}
