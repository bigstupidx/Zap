using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

public class DeadZone : MonoBehaviour {

    [SerializeField]
    [Tooltip("Origin position of dead zone that is where player moves to")]
    private Vector3 m_OriginPosition;
    [SerializeField]
    [Tooltip("Camera offset when in dead zone")]
    private Vector3 m_CamOffset;
    [SerializeField]
    [Tooltip("How long it takes to reach cam offset")]
    private float m_LerpTimeToCamOffset = 2.0f;

    void Start()
    {
        GameMaster.Instance.m_DeathStar.enabled = false;
        GameMaster.Instance.m_WarpParticleSystem.Play();
        GameMaster.Instance.m_CameraFollow.SetOffset(m_CamOffset, m_LerpTimeToCamOffset);
    }

    public Vector3 GetOriginOffsetPosition()
    {
        return m_OriginPosition;
    }
}
