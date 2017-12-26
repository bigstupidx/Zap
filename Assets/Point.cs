using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Point : MonoBehaviour {

    public float m_LerpTime = 0.9f;
    [SerializeField]
    private GameObject m_targetGO;
    private int m_NumPoints;

    private Vector3 startPosition;

    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        startPosition = this.transform.position;
        if(m_targetGO != null)
        {
            StartCoroutine(lerpToPosition());
        }
    }

    public void SetColor(Color color)
    {

    }

    public void SetTarget(GameObject go)
    {
        startPosition = this.transform.position;
        m_targetGO = go;
        StartCoroutine(lerpToPosition());
    }

    public void SetNumPoints(int points)
    {
        m_NumPoints = points;
    }

    private IEnumerator lerpToPosition()
    {
        float currTime = 0.0f;
        while(currTime < m_LerpTime)
        {
            currTime += Time.deltaTime;
            Vector3 targetWorldSpacePosition = m_targetGO.transform.position;
            this.transform.position = Vector3.Lerp(startPosition, targetWorldSpacePosition, currTime / m_LerpTime);
            yield return null;
        }
    }
}
