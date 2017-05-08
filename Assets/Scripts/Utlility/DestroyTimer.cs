using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class DestroyTimer : MonoBehaviour
    {

        [SerializeField]
        private float m_LifeTime = 2.0f;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(DestroyAfterTime(m_LifeTime));
        }

        // Update is called once per frame
        IEnumerator DestroyAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }
    }
}