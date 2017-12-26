using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : SolarObject {

    [SerializeField]
    private float minSize = 1.0f;
    [SerializeField]
    private float maxSize = 3.0f;

    public float lifeTime = 10.0f;
    private Vector3 startScale;

    void Start () {
        float randomSize = Random.Range(minSize, maxSize);
        this.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        StartCoroutine(destroyAfterShrink());
        startScale = new Vector3(randomSize, randomSize, randomSize);
	}

    private IEnumerator destroyAfterShrink()
    {
        float currTime = 0.0f;
        while (currTime < lifeTime)
        {
            currTime += Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, currTime / lifeTime);
            yield return null;
        }
    }
}
