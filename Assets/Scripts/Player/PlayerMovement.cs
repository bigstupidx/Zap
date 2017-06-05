using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameCritical;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public enum MovementState
        {
            MovingVertical,
            MovingHorizontal,
            MovingToWarpZone,
            MovingToZapGrid,
            MovingRocketJump
        }
        private MovementState m_MovementState;
        private MovementState m_PrevMovementState;
        private bool m_IsMovingRight;

        [SerializeField]
        private float m_HorizontalMoveSpeed = 0.05f;
        [SerializeField]
        private float m_VerticalMoveSpeed = 0.1f;
        [SerializeField]
        private LayerMask m_TouchInputMask;
        [SerializeField]
        private ParticleSystem m_FakeTrailParticleSystem;

        private Vector3 m_TargetPosition;
        private Vector3 m_StartPosition;
        private float m_SpeedMultiplier = 1.0f;
        private float m_LerpAmount = 0.0f;
        private float m_LerpTime = 1.0f;
        private float m_LerpPercentage = 0.0f;
        private Zap m_CurrZap;
        private Zap m_NextZap;
        private int m_CurrRow;
        private int m_CurrCol;
        private int m_NextCol;
        private bool m_CanMove; // if true then cannot go to next line

        private Rigidbody2D m_Rigidbody;
        private TrailRenderer m_TrailRenderer;
        private SpriteRenderer m_SpriteRenderer;
        private PlayerScaler m_PlayerScaler;

        // Use this for initialization
        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_TrailRenderer = GetComponent<TrailRenderer>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_PlayerScaler = GetComponent<PlayerScaler>();
            MoveToZapGrid();
            m_TrailRenderer.sortingLayerName = "Foreground";
            m_TrailRenderer.sortingOrder = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_CanMove)
            {
                if (m_CurrZap != null && m_NextZap != null)
                {
                    // TOUCH INPUT
                    for (int i = 0; i < Input.touchCount; ++i)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            SetMovementState(MovementState.MovingVertical);
                            fillMovementData();
                        }
                    }

                    // PC INPUT
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        SetMovementState(MovementState.MovingVertical);
                        fillMovementData();
                    }
                }
            }

            if(m_MovementState == MovementState.MovingHorizontal ||
                m_MovementState == MovementState.MovingToZapGrid)
            {
                if (m_NextZap == null)
                {
                    fillMovementData();
                }
            }

            lerpToTarget();
        }

        public void SetMovementState(MovementState movementState)
        {
            m_PrevMovementState = m_MovementState;
            m_MovementState = movementState;
        }

        public void SetSpeedMultiplier(float multiplier, bool canMove)
        {
            m_SpeedMultiplier = multiplier;
            m_CanMove = canMove;
        }

        private void lerpToTarget()
        {
            float startToFinishDistance = (m_TargetPosition - m_StartPosition).magnitude;

            if (m_MovementState == MovementState.MovingHorizontal)
            {
                // Lerp normal
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_HorizontalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
            }
            else if (m_MovementState == MovementState.MovingVertical)
            {
                // Double lerp
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_VerticalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
                //this.transform.position = Vector3.Lerp(Vector3.Lerp(m_StartPosition, m_P1, m_LerpPercentage),
                //  Vector3.Lerp(m_StartPosition, m_P2, m_LerpPercentage), m_LerpPercentage);
            }
            else if (m_MovementState == MovementState.MovingToWarpZone)
            {
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_VerticalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
                // Lerp size of player to original size
                m_PlayerScaler.LerpToOriginalScale(m_LerpPercentage);
            }
            else if (m_MovementState == MovementState.MovingToZapGrid)
            {
                DeathStar deathStar = GameMaster.Instance.m_DeathStar;
                if (deathStar)
                {
                    deathStar.SetIsMoving(false);
                }
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_VerticalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
                // Scale player to match zap grid size.
                m_PlayerScaler.LerpToZapScale(m_LerpPercentage);
            }
            else if (m_MovementState == MovementState.MovingRocketJump)
            {
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_VerticalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
            }

            // check to see if we reached target
            if (m_LerpPercentage >= 1.0f)
            {
                m_PlayerScaler.ResetPlayerScaler();
                decideNextMovementType();
                SetSpeedMultiplier(1.0f, true);
                m_LerpAmount = 0.0f;
                fillMovementData();

                // If we get to the end of the grid then go to warp zone
                if (m_CurrZap != null)
                {
                    if (m_CurrZap.GetComponent<EndZap>())
                    {
                        MoveToWarpZone();
                    }
                }
            }
        }

        private void decideNextMovementType()
        {
            if (m_MovementState == MovementState.MovingHorizontal)
            {
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingVertical)
            {
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingToWarpZone)
            {
                m_TargetPosition = this.transform.position;
                m_StartPosition = this.transform.position;
                m_FakeTrailParticleSystem.gameObject.SetActive(true);
                GameMaster.Instance.m_WarpZoneManager.SetInputEnabled(true);
                GameMaster.Instance.m_UIManager.m_WarpStorePanel.Show();
            }
            else if (m_MovementState == MovementState.MovingToZapGrid)
            {
                SetMovementState(MovementState.MovingHorizontal);
                DeathStar deathStar = GameMaster.Instance.m_DeathStar;
                if (deathStar)
                {
                    deathStar.ResetPosition();
                    deathStar.SetIsMoving(true);
                }
            }
            else if (m_MovementState == MovementState.MovingRocketJump)
            {
                GameMaster.Instance.m_PlayerStats.SetInvicible(false);
                SetMovementState(MovementState.MovingHorizontal);
            }
        }

        // gets called when we reach a zap (one time per zap)
        private void fillMovementData()
        {
            if (m_MovementState == MovementState.MovingHorizontal)
            {
                MoveHorizontally();
            }
            else if (m_MovementState == MovementState.MovingVertical)
            {
                Zap zapMovedThrough = getZapCurrentlyUnderneath();
                bool isRocketZap = zapMovedThrough is RocketZap;
                if(!isRocketZap)
                {
                    MoveVertically();
                }
                zapMovedThrough.ApplyImmediateEffect();
            }
            else if (m_MovementState == MovementState.MovingToZapGrid)
            {
                ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
                m_TargetPosition = currZapGrid.GetZap(0, 0).GetOffsetPosition();
            }
        }

        public void MoveHorizontally()
        {
            // switch next and curr zap if we have a next zap
            if (m_NextZap != null)
            {
                m_StartPosition = m_NextZap.GetOffsetPosition();
                m_CurrZap = m_NextZap;
                m_CurrCol = m_NextCol;
            }

            // check to make sure we don't go out of bounds
            ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
            if (currZapGrid != null)
            {
                if (m_IsMovingRight)
                {
                    if (m_NextCol + 1 < currZapGrid.GetNumCols(m_CurrRow))
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

                m_NextZap = currZapGrid.GetZap(m_CurrRow, m_NextCol);
                m_TargetPosition = m_NextZap.GetOffsetPosition();
                m_StartPosition = this.transform.position;
            }
        }

        private bool underneathCurrentZap()
        {
            bool res = transform.position.x <= (m_CurrZap.transform.position.x + m_CurrZap.Width) &&
                transform.position.x >= m_CurrZap.transform.position.x;
            return res;
        }

        private Zap getZapCurrentlyUnderneath()
        {
            if(underneathCurrentZap())
            {
                return m_CurrZap;
            }
            else
            {
                return m_NextZap;
            }
        }

        // returns the zap we move through initially when moving up.
        public Zap MoveVertically()
        {
            ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();

            if (currZapGrid)
            {
                // don't allow player to jump higher than number of rows in zap grid.
                //m_CurrRow = Mathf.Clamp(m_CurrRow, 0, currZapGrid.GetNumRows());
                m_CurrRow++;


                /* get correct zap on new line to go to 
                    this will compare the distance between the curr and next zap
                    if next zap is closer then go to it and vice versa. */
                Zap zapOnNewLine = null;
                Zap zapMovedThrough = null;
                bool underneathCurrZap = underneathCurrentZap();
                if (underneathCurrZap)
                {
                    // handles to make sure that we can't repeatedly make up for increment issues when moving vertically.
                    if (m_PrevMovementState != MovementState.MovingVertical)
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
                    zapOnNewLine = currZapGrid.GetZap(m_CurrRow, m_NextCol);
                    zapMovedThrough = m_CurrZap;
                }
                else
                {
                    zapOnNewLine = currZapGrid.GetZap(m_CurrRow, m_NextCol);
                    zapMovedThrough = m_NextZap;
                }


                if (zapOnNewLine != null)
                {
                    //m_P2 = m_CurrZap.GetOffsetPosition();
                    //m_P1 = m_StartPosition
                    //m_TargetPosition = GetPoint(m_LerpPercentage);
                    // figure out which zap we are closer too
                    m_CurrZap = zapOnNewLine;
                    m_NextZap = zapOnNewLine;
                    //m_CurrRow += numRows;
                    m_LerpAmount = 0.0f;
                    m_StartPosition = this.transform.position;
                    m_TargetPosition = zapOnNewLine.GetOffsetPosition();
                }
                else
                {
                    MoveToWarpZone();
                }

                return zapMovedThrough;
            }

            return null;
        }

        public void MoveTo(Zap targetZap)
        {
            m_CurrZap = targetZap;
            m_NextZap = targetZap;
            m_NextCol = targetZap.Col;
            m_CurrRow = targetZap.Row;
            m_StartPosition = this.transform.position;
            m_TargetPosition = targetZap.GetOffsetPosition();
            SetMovementState(MovementState.MovingHorizontal);
            SetSpeedMultiplier(1.0f, false);
            m_LerpAmount = 0.0f;
        }

        public void MoveRocketJump(int numRows)
        {
            ZapGrid currGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
            if(currGrid)
            {
                m_CurrRow += numRows;
                m_CurrRow = Mathf.Clamp(m_CurrRow, 0, currGrid.GetNumRows() - 1);

                bool underneathCurrZap = underneathCurrentZap();
                Zap next  = m_NextZap;
                Zap curr = m_CurrZap;
                if (underneathCurrZap)
                {
                    m_NextZap = currGrid.GetZap(m_CurrRow, m_CurrCol);
                }
                else
                {
                    m_NextZap = currGrid.GetZap(m_CurrRow, m_NextCol);
                }
                m_CurrZap = m_NextZap;
                m_TargetPosition = m_NextZap.GetOffsetPosition();
                m_StartPosition = this.transform.position;
                m_LerpAmount = 0.0f;
            }
        }

        public void MoveToWarpZone()
        {
            // Move to next zap grid
            WarpZone newDeadZone = GameMaster.Instance.m_WarpZoneManager.SpawnDeadZone();
            m_CurrZap = null;
            m_NextZap = null;
            m_NextCol = 0;
            m_CurrRow = 0;
            m_StartPosition = this.transform.position;
            m_TargetPosition = newDeadZone.transform.position;
            SetMovementState(MovementState.MovingToWarpZone);
            SetSpeedMultiplier(0.45f, false);
            m_LerpAmount = 0.0f;
            GameMaster.Instance.m_BackDropManager.ShowWarpStoreColors();

            // show flawless completion notification if grid completion flawless.
            StatsManager statsManager = GameMaster.Instance.m_StatsManager;
            if (statsManager && statsManager.GetFlawlessGridRun())
            {
                UIManager uiManager = GameMaster.Instance.m_UIManager;
                if(uiManager)
                {
                    uiManager.SpawnUINotification("FLAWLESS RUN!\n+500 pts", true);
                    statsManager.AddToScore(500);
                }
            }
        }

        public void MoveToZapGrid()
        {
            m_CurrZap = null;
            m_NextZap = null;
            m_PrevMovementState = MovementState.MovingToZapGrid;
            m_MovementState = MovementState.MovingToZapGrid;
            m_StartPosition = this.transform.position;
            m_SpeedMultiplier = 1.0f;
            m_IsMovingRight = true;
            m_CurrRow = 0;
            m_CurrCol = 0;
            m_CanMove = true;
            m_FakeTrailParticleSystem.gameObject.SetActive(false);
        }
    }
}