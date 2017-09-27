using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(_2dxFX_AL_PixelDie))]
public class PixelDieDriver : MonoBehaviour
{

    private _2dxFX_AL_PixelDie _pixelDieEffect;

    [SerializeField]
    private float _pixelEffectTimeMin = 12.0f;
    [SerializeField]
    private float _pixelEffectTimeMax = 20.0f;

    void Awake()
    {
        _pixelDieEffect = GetComponent<_2dxFX_AL_PixelDie>();
        StartCoroutine("turnToSmoke");
    }

    private IEnumerator turnToSmoke()
    {
        float currTime = 0.0f;
        float randomDestroyTime = Random.Range(_pixelEffectTimeMin, _pixelEffectTimeMax);
        while (currTime < randomDestroyTime)
        {
            currTime += Time.deltaTime;
            _pixelDieEffect._Value1 = currTime / randomDestroyTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
