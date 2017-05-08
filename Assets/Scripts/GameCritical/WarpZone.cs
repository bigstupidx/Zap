using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace GameCritical
{
    public class WarpZone : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Origin position of dead zone is the offset from the top of the last zap grid and is where the player moves to")]
        private Vector3 m_OriginPosition;
        [SerializeField]
        [Tooltip("Camera offset when in dead zone")]
        private Vector3 m_CamOffset;
        [SerializeField]
        [Tooltip("How long it takes to reach cam offset")]
        private float m_LerpTimeToCamOffset = 2.0f;

        void Start()
        {
            GameMaster.Instance.m_CameraFollow.SetOffset(m_CamOffset, m_LerpTimeToCamOffset);
            GameMaster.Instance.m_DeathStar.SetIsMoving(false);
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
