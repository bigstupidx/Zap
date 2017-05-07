using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Rotate : MonoBehaviour
    {

        [SerializeField]
        private float m_RotateSpeed = 1.0f;

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(0, 0, 1 * Time.deltaTime * m_RotateSpeed);
        }
    }
}
