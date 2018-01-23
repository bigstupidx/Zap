using GameCritical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreMenuPanel : UIPanel
    {
        public Text m_Score;
        public Text m_Stage;
        [SerializeField]
        private float m_ExpandTime = 1.0f;
        [SerializeField]
        private float m_OnScreenTime = 3.0f;
        public bool menuIsShowing = false;

        public void SetScoreText(int score)
        {
            if(m_Score)
                m_Score.text = score.ToString();
        }

        public void SetStageText(int stage)
        {
            if(m_Stage)
                m_Stage.text = "STAGE: " + stage.ToString();
        }

        public override void Show()
        {
            base.Show();
            this.gameObject.SetActive(true);
            GameCritical.GameMaster.Instance.m_UIManager.m_ShopCanvas.gameObject.SetActive(false);
            //GameCritical.GameMaster.Instance.EndGame();
            StartCoroutine(expandScoreMenu());
        }

        private void Update()
        {
            if(menuIsShowing)
            {
                // TOUCH INPUT
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        GameCritical.GameMaster.Instance.EndGame();
                    }
                }

                // PC INPUT
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameCritical.GameMaster.Instance.EndGame();
                }
            }
        }

        private IEnumerator waitOnScreenForTime()
        {
            yield return new WaitForSeconds(m_OnScreenTime);
            GameCritical.GameMaster.Instance.EndGame();
        }

        private IEnumerator expandScoreMenu()
        {
            float currTime = 0.0f;
            while(currTime < m_ExpandTime)
            {
                currTime += Time.deltaTime;
                this.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(1, 1, 1), currTime / m_ExpandTime);
                yield return null;
            }

            menuIsShowing = true;

            // if we are logged in then get high score information
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_PARAM_EMAIL))
            {
                yield return StartCoroutine(GameMaster.Instance.m_DatabaseManager.m_DataInserter.SetHighScore(
                    SaveManager.GetString(Database.DatabaseConstants.m_PARAM_EMAIL),
                    GameMaster.Instance.m_StatsManager.GetScore(),
                    setHighScoreSuccesfully,
                    setHighScoreFailed
                    ));
            }
            StartCoroutine(waitOnScreenForTime());
        }

        private void setHighScoreFailed()
        {
            Debug.Log("SET HIGH SCORE FAILED!");
        }

        private void setHighScoreSuccesfully()
        {
            Debug.Log("SET HIGH SCORE SUCCESS!");
        }
    }
}
