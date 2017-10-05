using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

[RequireComponent(typeof(SpriteRenderer))]
public class SolarObject : MonoBehaviour
{
    private float _fadeInTime = 4.5f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeIn()
    {
        float currTime = 0.0f;
        while(currTime < _fadeInTime)
        {
            currTime += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(Color.clear, Color.white, currTime/_fadeInTime);
            yield return null;
        }
    }
}
