using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameCritical;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerDecorations))]
    [RequireComponent(typeof(PlayerScaler))]
    public class PlayerMovement : MonoBehaviour
    {
        public enum MovementState
        {
            MovingVertical,
            MovingHorizontal,
            MovingToWarpZone,
            MovingToZapGrid,
            MovingRocketJump,
            MovingBounceBackFromZap,
        }
        private MovementState m_MovementState;
        private MovementState m_PrevMovementState;
        private bool m_IsMovingRight;

        [SerializeField]
        private float m_HorizontalMoveSpeed = 540.0f;
        [SerializeField]
        private float m_VerticalMoveSpeed = 540.0f;
        [SerializeField]
        private float m_ToWarpZoneSpeed = 100.0f;
        [SerializeField]
        private LayerMask m_TouchInputMask;


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
        private SpriteRenderer m_SpriteRenderer;
        private PlayerScaler m_PlayerScaler;
        private PlayerDecorations m_PlayerDecorations;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_PlayerScaler = GetComponent<PlayerScaler>();
            m_PlayerDecorations = GetComponent<PlayerDecorations>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_CanMove) // don't allow payer to move up until they reach the grid.
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

            /*
             * FORCES PLAYER TO START AT FIRST ZAP WHEN APPROACHING ZAP GRID.
             * if(m_MovementState == MovementState.MovingHorizontal ||
                m_MovementState == MovementState.MovingToZapGrid)
            {
                if (m_NextZap == null)
                {
                    fillMovementData();
                }
            }*/

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
            // make sure target and start position are set before lerping
            if(m_TargetPosition == null || m_StartPosition == null)
            {
                return;
            }

            float startToFinishDistance = (m_TargetPosition - m_StartPosition).magnitude;

            if (m_MovementState == MovementState.MovingHorizontal || m_MovementState == MovementState.MovingVertical ||
                m_MovementState == MovementState.MovingRocketJump || m_MovementState == MovementState.MovingBounceBackFromZap)
            {
                // Lerp normal
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_HorizontalMoveSpeed;
                m_LerpPercentage = m_LerpAmount / startToFinishDistance;
                this.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_LerpPercentage);
            }
            else if (m_MovementState == MovementState.MovingToWarpZone)
            {
                m_LerpAmount += Time.deltaTime * m_SpeedMultiplier * m_ToWarpZoneSpeed;
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

            // check to see if we reached target
            if (m_LerpPercentage >= 1.0f)
            {
                m_PlayerScaler.ResetPlayerScaler();
                decideNextMovementType();
                //SetSpeedMultiplier(1.0f, true);
                m_LerpAmount = 0.0f;
                fillMovementData();

                // If we get to the end of the grid then go to warp zone
                /*if (m_CurrZap != null)
                {
                    if (m_CurrZap.GetComponent<EndZap>())
                    {
                        MoveToWarpZone();
                    }
                }*/
            }
        }

        private void decideNextMovementType()
        {
            if (m_MovementState == MovementState.MovingHorizontal)
            {
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingBounceBackFromZap)
            {
                SetSpeedMultiplier(1.0f, true);
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingVertical)
            {
                ZapGrid currZapGrid = GameMaster.Instance.m_ZapManager.GetZapGrid();
                if (m_CurrRow == currZapGrid.GetNumRows())
                {
                    MoveToWarpZone();
                    return;
                }
                SetMovementState(MovementState.MovingHorizontal);
            }
            else if (m_MovementState == MovementState.MovingToWarpZone)
            {
                m_TargetPosition = this.transform.position;
                m_StartPosition = this.transform.position;
                if(!m_PlayerDecorations.GetWarpZonePS().isPlaying)
                {
                    m_PlayerDecorations.ShowWarpZonePS();
                }
                GameMaster.Instance.m_UIManager.m_ShopCanvas.m_WarpStorePanel.Show();
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
                m_CanMove = true;
                if(m_PlayerDecorations != null)
                {
                    m_PlayerDecorations.DeactivateLockdown();
                    m_PlayerDecorations.DeactivateAtmosphereFire();
                }
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
                if (!isRocketZap)
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
                m_CurrRow++;
                // don't allow player to jump higher than number of rows in zap grid. Else argumentoutofrange exception
                m_CurrRow = Mathf.Clamp(m_CurrRow, 0, currZapGrid.GetNumRows());


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

                return zapMovedThrough;
            }

            return null;
        }

        public void MoveTo(Zap targetZap, MovementState movementState)
        {
            m_CurrZap = targetZap;
            m_NextZap = targetZap;
            m_NextCol = targetZap.Col;
            m_CurrRow = targetZap.Row;
            m_StartPosition = this.transform.position;
            m_TargetPosition = targetZap.GetOffsetPosition();
            SetMovementState(movementState);
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

                // don't let player move while rocket jumpin
                m_CanMove = false;
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
            SetSpeedMultiplier(2.0f, false);
            m_LerpAmount = 0.0f;
            GameMaster.Instance.m_BackDropManager.ShowWarpStoreColors();
            GameMaster.Instance.m_DadEventManager.StopEvents();
            GameMaster.Instance.m_SolarSystemSpawner.StopSpawningSolarObjects();

            // show flawless completion notification if grid completion flawless.
            StatsManager statsManager = GameMaster.Instance.m_StatsManager;
            if (statsManager && statsManager.GetFlawlessGridRun())
            {
                UIManager uiManager = GameMaster.Instance.m_UIManager;
                if(uiManager)
                {
                    uiManager.SpawnUINotification("FLAWLESS RUN [+500 PTS]", true);
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
            m_PlayerDecorations.ShowMainPS();
            GameMaster.Instance.m_DadEventManager.StartEvents();

            // Increase level UI displayed to user
            GameMaster.Instance.m_UIManager.m_LevelPanel.IncrementLevelText();

            // start cooldown of player boost if it exists
            GameMaster.Instance.m_PlayerBoost.Reset();

            // allow player to active the equipped ability
            GameMaster.Instance.m_PlayerBoost.canActivate = true;

            GameMaster.Instance.m_SolarSystemSpawner.
                BeginSpawningSolarObjects();

        }
    }
}