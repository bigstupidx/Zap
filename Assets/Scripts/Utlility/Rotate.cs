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
        private Vector3 m_RotateAxis = new Vector3(0, 0, 1);

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(m_RotateAxis * m_RotateSpeed * Time.deltaTime);
        }
    }
}
