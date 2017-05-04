using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    private enum MovementState
    {
        AcrossZapLine,
        NextZapLine
    }
    private MovementState m_MovementState;

    [SerializeField]
    private float m_HorizontalMoveSpeed = 0.05f;
    [SerializeField]
    private float m_HorizontalLerpTime = 0.5f;
    [SerializeField]
    private float m_VerticalMoveSpeed = 0.1f;
    [SerializeField]
    private float m_VerticalLerpTime = 2.0f;
    [SerializeField]
    private LayerMask m_TouchInputMask;

    private Vector3 m_TargetPosition;
    private Vector3 m_StartPosition;
    private Vector3 m_P1;
    private Vector3 m_P2;
    private float m_SpeedMultiplier = 1.0f;
    private float m_LerpAmount = 0.0f;
    private float m_LerpPercentage = 0.0f;

    private Zap m_CurrZap;
    private ZapLine m_CurrZapLine;
    private Rigidbody2D m_Rigidbody;

    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_MovementState = MovementState.AcrossZapLine;
        m_StartPosition = this.transform.position;
        m_SpeedMultiplier = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_MovementState = MovementState.NextZapLine;
            fillMovementData();
        }
        moveToZap();
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        m_SpeedMultiplier = multiplier;
    }

    private void moveToZap()
    {
        if (m_CurrZap == null)
        {
            fillMovementData();
        }

        lerpToTarget();

        // check to see if we reached target
        if (m_LerpPercentage >= 1.0f)
        {
            fillMovementData(); // gets zap for next lerp
            SetSpeedMultiplier(1.0f); // set speed multiplier back to normal if it was changed.
        }
    }

    private void lerpToTarget()
    {
        switch (m_MovementState)
        {
            case MovementState.AcrossZapLine:
                // Lerp
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier;
                m_LerpPercentage = m_LerpAmount / m_HorizontalLerpTime;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
                break;
            case MovementState.NextZapLine:
                // Double lerp
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier;
                m_LerpPercentage = m_LerpAmount / m_HorizontalLerpTime;
                this.transform.position = Vector3.Lerp(Vector3.Lerp(m_StartPosition, m_P1, m_LerpPercentage),
                    Vector3.Lerp(m_StartPosition, m_P2, m_LerpPercentage), m_LerpPercentage);
                break;
        }
    }

    private void fillMovementData()
    {
        switch (m_MovementState)
        {
            case MovementState.AcrossZapLine:
                // if we have a current zap then set start position
                if (m_CurrZap != null)
                {
                    m_StartPosition = m_CurrZap.GetOffsetPosition();
                }
                m_CurrZap = GameMaster.Instance.m_ZapManager.GetNextZap();
                m_LerpAmount = 0.0f;
                m_TargetPosition = m_CurrZap.GetOffsetPosition();
                break;
            case MovementState.NextZapLine:
                // if there is another zap line then set movement data accordingly
                ZapLine newZapline = GameMaster.Instance.m_ZapManager.GetNextZapLine();
                if (newZapline != null)
                {
                    //m_P2 = m_CurrZap.GetOffsetPosition();
                    //m_P1 = m_StartPosition
                    //m_TargetPosition = GetPoint(m_LerpPercentage);
                    m_LerpAmount = 0.0f;
                    m_CurrZapLine = newZapline;
                    m_CurrZap.ApplyEffect();
                    m_StartPosition = this.transform.position;
                    m_CurrZap = GameMaster.Instance.m_ZapManager.GetCurrentZap();
                    m_TargetPosition = m_CurrZap.GetOffsetPosition();
                    SetSpeedMultiplier(2.2f);
                }

                // set movement state back to across zapline
                m_MovementState = MovementState.AcrossZapLine;
                break;
        }
    }
}