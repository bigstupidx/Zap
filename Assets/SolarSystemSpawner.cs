using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemSpawner : MonoBehaviour {

    [Header("Solar objects")]
    public List<SolarObject> solarObjects;
    [SerializeField]
    private float _minSpawnTime = 0.0f;
    [SerializeField]
    private float _maxSpawnTime = 5.0f;
    [SerializeField]
    private int _maxSolarObjects = 4;

    [Header("Stars")]
    [SerializeField]
    private SolarObject starSolarPrefab;
    [SerializeField]
    private float _minSpawnTimeStar = 0.0f;
    [SerializeField]
    private float _maxSpawnTimeStar = 5.0f;

    private int _numSolarObjects;

    private void Start()
    {
        StartSpawngingStars();
    }

    private void StartSpawngingStars()
    {
        float randomWaitTimeForStars = Random.Range(_minSpawnTimeStar, _maxSpawnTimeStar);
        Invoke("SpawnStar", randomWaitTimeForStars);
    }

    // Use this for initialization
    public void BeginSpawningSolarObjects () {
        _numSolarObjects = 0;
        if(solarObjects.Count > 0)
        {
            float randomWaitTime = Random.Range(_minSpawnTime, _maxSpawnTime);
            Invoke("SpawnSolarObject", randomWaitTime);
        }
    }

    public void StopSpawningSolarObjects()
    {
        CancelInvoke();
        StartSpawngingStars();
    }

    private void SpawnStar()
    {
        float randomWaitTimeForStars = Random.Range(_minSpawnTimeStar, _maxSpawnTimeStar);
        Invoke("SpawnStar", randomWaitTimeForStars);

        Vector3 additionalHeightVector = new Vector3(0, Utility.ScreenUtilities.GetDistanceInWS(0.2f), 0);
        Vector3 aboveTopLeftOfScreen = Utility.ScreenUtilities.GetWSofSSPosition(0.0f, 1.0f) + additionalHeightVector;
        Vector3 aboveTopRightOfScreen = Utility.ScreenUtilities.GetWSofSSPosition(1.0f, 1.0f) + additionalHeightVector;
        float randomRangeBetweenLeftAndRight = Random.Range(0, 1.0f);
        Vector3 spawnPos = Vector3.Lerp(aboveTopLeftOfScreen, aboveTopRightOfScreen, randomRangeBetweenLeftAndRight);
        spawnPos.z = -80;

        Star solarObjectInstance = (Star)Instantiate(starSolarPrefab, spawnPos, Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    private void SpawnSolarObject()
    {
        // don't continue coroutine if no objects to spawn
        if (solarObjects.Count <= 0 || _numSolarObjects >= _maxSolarObjects)
        {
            return;
        }
        //yield return TimerHelper.instance.StartTimer(randomWaitTime);
        int randomIndex = Random.Range(0, solarObjects.Count);

        // Get spawn parameters set
        SolarObject solarObjectPrefab = solarObjects[randomIndex];
        Vector3 additionalHeightVector = new Vector3(0, Utility.ScreenUtilities.GetDistanceInWS(0.2f), 0);
        Vector3 aboveTopLeftOfScreen = Utility.ScreenUtilities.GetWSofSSPosition(0.0f, 1.0f) + additionalHeightVector;
        Vector3 aboveTopRightOfScreen = Utility.ScreenUtilities.GetWSofSSPosition(1.0f, 1.0f) + additionalHeightVector;
        float randomRangeBetweenLeftAndRight = Random.Range(0, 1.0f);
        Vector3 spawnPos = Vector3.Lerp(aboveTopLeftOfScreen, aboveTopRightOfScreen, randomRangeBetweenLeftAndRight);

        // Spawn solar object
        spawnPos.z = -80;
        SolarObject solarObjectInstance = Instantiate(solarObjectPrefab, spawnPos, Quaternion.identity, this.transform);
        _maxSolarObjects++;

        float randomWaitTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        Invoke("SpawnSolarObject", randomWaitTime);
    }
}
