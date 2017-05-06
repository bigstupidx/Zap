using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    private enum MovementState
    {
        MovingVertical,
        MovingHorizontal
    }
    private MovementState m_MovementState;
    private MovementState m_PrevMovementState;
    private bool m_IsMovingRight;

    [SerializeField]
    private float m_HorizontalMoveSpeed = 0.05f;
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
    private float m_LerpTime = 1.0f;
    private float m_LerpPercentage = 0.0f;
    private Zap m_CurrZap;
    private Zap m_NextZap;
    private int m_CurrRow;
    private int m_CurrCol;
    private int m_NextCol;

    private Rigidbody2D m_Rigidbody;

    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        SetMovementState(MovementState.MovingHorizontal);
        m_StartPosition = this.transform.position;
        m_SpeedMultiplier = 1.0f;
        m_IsMovingRight = true;
        m_CurrRow = 0;
        m_CurrCol = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_CurrZap != null && m_NextZap != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetMovementState(MovementState.MovingVertical);
                fillMovementData();
            }
        }

        if (m_NextZap == null)
        {
            fillMovementData();
        }

        lerpToTarget();
    }

    private void SetMovementState(MovementState movementState)
    {
        m_PrevMovementState = m_MovementState;
        m_MovementState = movementState;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        m_SpeedMultiplier = multiplier;
    }

    private void lerpToTarget()
    {
        // add to lerp amount
        m_LerpAmount += Time.deltaTime * m_SpeedMultiplier;
        m_LerpPercentage = m_LerpAmount / m_LerpTime;

        if (m_MovementState == MovementState.MovingHorizontal)
        {
            // Lerp normal
            this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
        }
        else if(m_MovementState == MovementState.MovingVertical)
        {
            // Double lerp
            this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
            //this.transform.position = Vector3.Lerp(Vector3.Lerp(m_StartPosition, m_P1, m_LerpPercentage),
              //  Vector3.Lerp(m_StartPosition, m_P2, m_LerpPercentage), m_LerpPercentage);
        }

        // check to see if we reached target
        if (m_LerpPercentage >= 1.0f)
        {
            if (m_MovementState == MovementState.MovingHorizontal)
            {
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingVertical)
            {
                SetMovementState(MovementState.MovingHorizontal);
            }

            m_LerpAmount = 0.0f;
            fillMovementData(); // gets zap for next lerp
        }
    }

    // gets called when we reach a zap (one time per zap)
    private void fillMovementData()
    {
        if (m_MovementState == MovementState.MovingHorizontal)
        {
            SetSpeedMultiplier(m_HorizontalMoveSpeed);

            // switch next and curr zap if we have a next zap
            if (m_NextZap != null)
            {
                m_StartPosition = m_NextZap.GetOffsetPosition();
                m_CurrZap = m_NextZap;
                m_CurrCol = m_NextCol;
            }

            // check to make sure we don't go out of bounds
            if (m_IsMovingRight)
            {
                if (m_NextCol + 1 < GameMaster.Instance.m_ZapManager.GetZapGrid().GetNumCols(m_CurrRow))
                {
                    m_NextCol++;
                }
                else
                {
                    m_IsMovingRight = false;
                    m_NextCol--;
                }
            }
            else
            {
                if (m_NextCol - 1 >= 0)
                {
                    m_NextCol--;
                }
                else
                {
                    m_IsMovingRight = true;
                    m_NextCol++;
                }
            }

            m_NextZap = GameMaster.Instance.m_ZapManager.GetZapGrid().GetZap(m_CurrRow, m_NextCol);
            m_TargetPosition = m_NextZap.GetOffsetPosition();
        }
        else if (m_MovementState == MovementState.MovingVertical)
        {
            SetSpeedMultiplier(m_VerticalMoveSpeed);

            /* get correct zap on new line to go to 
             this will compare the distance between the curr and next zap
             if next zap is closer then go to it and vice versa. */
            Zap zapOnNewLine = null;
            bool underneathCurrZap =  transform.position.x <= (m_CurrZap.transform.position.x + m_CurrZap.Width) &&
                transform.position.x >= m_CurrZap.transform.position.x;
            if (underneathCurrZap)
            {
                // handles to make sure that we can't repeatedly make up for increment issues when moving vertically.
                if(m_PrevMovementState != MovementState.MovingVertical)
                {
                    if (m_IsMovingRight)
                    {
                        m_NextCol--;
                    }
                    else
                    {
                        m_NextCol++;
                    }
                }
                zapOnNewLine = GameMaster.Instance.m_ZapManager.GetZapGrid().GetZap(m_CurrRow + 1, m_NextCol);

            }
            else
            { 
                zapOnNewLine = GameMaster.Instance.m_ZapManager.GetZapGrid().GetZap(m_CurrRow + 1, m_NextCol);
            }


            if (zapOnNewLine != null)
            {
                //m_P2 = m_CurrZap.GetOffsetPosition();
                //m_P1 = m_StartPosition
                //m_TargetPosition = GetPoint(m_LerpPercentage);
                // figure out which zap we are closer too
                m_CurrZap = m_NextZap;
                m_NextZap = zapOnNewLine;
                m_CurrRow++;
                m_LerpAmount = 0.0f;
                m_CurrZap.ApplyEffect();
                m_StartPosition = this.transform.position;
                m_TargetPosition = zapOnNewLine.GetOffsetPosition();
            }
        }
    }
}