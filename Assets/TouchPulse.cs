using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TouchPulse : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _endScale = new Vector3(1,1,1);

        [SerializeField]
        private float _pulseLerpTime;

        private SpriteRenderer _spriteRenderer;

        // Use this for initialization
        void Start()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            StartCoroutine(pulseOut());
        }

        private IEnumerator pulseOut()
        {
            Vector3 startScale = this.transform.localScale;
            Color startColor = _spriteRenderer.color;
            float currTime = 0.0f;
            while(currTime < _pulseLerpTime)
            {
                currTime += Time.deltaTime;
                this.transform.localScale = Vector3.Lerp(startScale, _endScale, currTime / _pulseLerpTime);
                _spriteRenderer.color = Color.Lerp(startColor, new Color(1, 1, 1, 0), currTime / _pulseLerpTime);
                yield return null;
            }
            Destroy(this.gameObject);
        }
    }
}
