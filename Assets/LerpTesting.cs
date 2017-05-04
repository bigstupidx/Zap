using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTesting : MonoBehaviour {

    public float m_LerpTime = 1.0f;

    public Transform m_P1;
    public Transform m_P2;
    public Transform m_P3;

    public bool replay = true;

    private float m_CurrentLerpTime = 0.0f;
    private const int lineSteps = 10;

    // Use this for initialization
    void Start () {
		
	}

    public Vector3 GetPoint(float t)
    {
        return Vector3.Lerp(Vector3.Lerp(m_P1.position, m_P2.position, t), 
            Vector3.Lerp(m_P1.position, m_P3.position, t) , t);
    }

    // Update is called once per frame
    void Update () {
        //increment timer once per frame
        m_CurrentLerpTime += Time.deltaTime;
        if (m_CurrentLerpTime > m_LerpTime)
        {
            if(replay)
            {
                m_CurrentLerpTime = 0.0f;
            }
            else
            {
                m_CurrentLerpTime = m_LerpTime;
            }
        }

        float t = m_CurrentLerpTime / m_LerpTime;
        Vector3 targetLocation = GetPoint(t);
        this.transform.position = Vector3.Lerp(this.transform.position, targetLocation, t);
    }

    void OnDrawGizmos()
    {
        Vector3 lineStart = GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = GetPoint(i / (float)lineSteps);
            Debug.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }
    }

}
