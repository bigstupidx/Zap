using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelPanel : UIPanel {

        [SerializeField]
        private Text m_LevelText;

        [SerializeField]
        private string m_PreString = "LEVEL ";

        [SerializeField]
        private Vector3 m_MaxScale;

        [SerializeField]
        private float m_LerpTime = 1.0f;

        [SerializeField]
        private float m_PauseTime = 1.0f;

        private int m_OverallLevel;

        void Awake()
        {
            m_OverallLevel = 0;
            this.transform.localScale = new Vector3(0, 0, 0);
        }

        public void IncrementLevelText()
        {
            m_OverallLevel++;
            m_LevelText.text = m_PreString + m_OverallLevel.ToString();
            Show();
        }

        public override void Show()
        {
            base.Show();
            StartCoroutine(lerpScaleUpAndDown());
        }

        private IEnumerator lerpScaleUpAndDown()
        {
            yield return StartCoroutine(lerpScale(m_MaxScale));
            yield return new WaitForSeconds(m_PauseTime);
            yield return StartCoroutine(lerpScale(new Vector3(0, 0, 0)));
            Hide();
        }

        private IEnumerator lerpScale(Vector3 scale)
        {
            float lerpAmount = 0.0f;
            Vector3 startScale = this.transform.localScale;
            while(lerpAmount < 1.0f)
            {
                lerpAmount += Time.deltaTime;
                float lerpPercentage = lerpAmount / m_LerpTime;
                this.transform.localScale = Vector3.Lerp(startScale, scale, lerpPercentage);
                yield return null;
            }
        }
    }
}
