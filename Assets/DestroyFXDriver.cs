using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(_2dxFX_DestroyedFX))]
public class DestroyFXDriver : MonoBehaviour {

    private _2dxFX_DestroyedFX _destroyFX;

    [SerializeField]
    private float _destroyTimeMin = 12.0f;
    [SerializeField]
    private float _destroyTimeMax = 20.0f;

    void Awake ()
    {
        _destroyFX = GetComponent<_2dxFX_DestroyedFX>();
        StartCoroutine("destroyOverTime");
    }

    private IEnumerator destroyOverTime()
    {
        float currTime = 0.0f;
        float randomDestroyTime = Random.Range(_destroyTimeMin, _destroyTimeMax);
        while(currTime < randomDestroyTime)
        {
            currTime += Time.deltaTime;
            _destroyFX.Destroyed = currTime / randomDestroyTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
