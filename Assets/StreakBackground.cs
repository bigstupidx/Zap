using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakBackground : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public float colorLerpTime = 0.6f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(lerpColors());
	}
	
	// Update is called once per frame
	private IEnumerator lerpColors ()
    {
        float currTime = 0.0f;
        Color endColor = spriteRenderer.color;
        Color startColor = new Color(endColor.r, endColor.g, endColor.b, 0);
        while (currTime < colorLerpTime)
        {
            currTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, currTime / colorLerpTime);
            yield return null;
        }
	}
}
