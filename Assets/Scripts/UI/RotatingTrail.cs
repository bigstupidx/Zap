using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RotatingTrail : MonoBehaviour {

        public float m_RotateSpeed = 400.0f;

        void Update()
        {
            this.transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), m_RotateSpeed * Time.deltaTime);
        }
    }
}
