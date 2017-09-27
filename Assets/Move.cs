using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    [SerializeField]
    private Vector3 _moveDirection;

    [SerializeField]
    private float _moveSpeedMin = 1.0f;
    [SerializeField]
    private float _moveSpeedMax = 3.0f;
    private float _moveSpeed;

    [SerializeField]
    private bool _useLocalPosition;

    private void Awake()
    {
        _moveSpeed = Random.Range(_moveSpeedMin, _moveSpeedMax);
    }

    // Update is called once per frame
    void Update () {
        if(_useLocalPosition)
        {
            this.transform.localPosition += _moveDirection * _moveSpeed * Time.deltaTime;
        }
        else
        {
            this.transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }
	}
}
