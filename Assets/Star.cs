using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : SolarObject {

    [SerializeField]
    private float minSize = 1.0f;
    [SerializeField]
    private float maxSize = 3.0f;

    void Start () {
        float randomSize = Random.Range(minSize, maxSize);
        this.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
	}
}
