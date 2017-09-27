using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(_2dxFX_AL_Smoke))]
public class SmokeDriver : MonoBehaviour {

    private _2dxFX_AL_Smoke _smokeEffect;

    [SerializeField]
    private float _smokeEffectTimeMin = 12.0f;
    [SerializeField]
    private float _smokeEffectTimeMax = 20.0f;

    void Awake ()
    {
        _smokeEffect = GetComponent<_2dxFX_AL_Smoke>();
        StartCoroutine("turnToSmoke");
    }

    private IEnumerator turnToSmoke()
    {
        float currTime = 0.0f;
        float randomDestroyTime = Random.Range(_smokeEffectTimeMin, _smokeEffectTimeMax);
        while (currTime < randomDestroyTime)
        {
            currTime += Time.deltaTime;
            _smokeEffect._TurnToSmoke = currTime / randomDestroyTime;
            yield return null;
        }
        _smokeEffect._TurnToSmoke = 1.0f;
        Destroy(this.gameObject);
    }
}
