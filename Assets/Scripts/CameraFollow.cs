using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private PlayerMovement m_PlayerMovement;
    [SerializeField]
    private Vector3 m_Offset;
    [SerializeField]
    private float m_LerpTime;
    private float m_LerpAmount;

	// Use this for initialization
	void Start () {
        m_LerpAmount = 0.0f;
		if(m_PlayerMovement == null)
        {
            m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(m_PlayerMovement != null)
        {
            Vector3 target = m_PlayerMovement.transform.position;
            target.x = 0;
            target.z = 0;

            m_LerpAmount += Time.deltaTime;
            float lerpPercentage = m_LerpAmount / m_LerpTime;
            this.transform.position = Vector3.Lerp(this.transform.position, target + m_Offset, lerpPercentage);

            if (lerpPercentage >= 1.0f)
            {
                m_LerpAmount = 0.0f;
            }
        }
	}
}
