using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Rotate : MonoBehaviour
    {

        [SerializeField]
        private float m_RotateSpeed = 1.0f;

        [SerializeField]
        private bool m_RandomRotateDir = true;

        [SerializeField]
        private bool m_RandomRotateSpeed = true;

        [SerializeField]
        private Vector3 m_RotateAxis = new Vector3(0, 0, 1);

        private void Awake()
        {
            if(m_RandomRotateSpeed)
            {
                m_RotateSpeed = Random.Range(-m_RotateSpeed, m_RotateSpeed);
            }

            if(m_RandomRotateDir)
            {
                int dir = Random.Range(0, 1);
                m_RotateSpeed = (dir == 0) ? m_RotateSpeed : -m_RotateSpeed;
            }
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(m_RotateAxis * m_RotateSpeed * Time.deltaTime);
        }
    }
}
