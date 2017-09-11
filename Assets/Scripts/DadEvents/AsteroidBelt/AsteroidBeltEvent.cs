using System.Collections;
using UnityEngine;
using Utility;

namespace DadEvents
{
    public class AsteroidBeltEvent : DadEvent
    {
        [SerializeField]
        private Asteroid m_AsteroidPrefab;
        [SerializeField]
        private Blink m_ExclamationPrefab;

        [SerializeField]
        private Vector2 m_InitialForce;
        [SerializeField]
        private float m_AsteroidSpawnTime;
        [SerializeField]
        private float m_ExclamationTime;
        [SerializeField]
        [Range(0, 1)]
        private float m_UpperScreenBound;
        [SerializeField]
        [Range(0, 1)]
        private float m_LowerScreenBound;
        [SerializeField]
        [Range(2.0f, 4.0f)]
        private float m_BlinkIntervalMultiplierShortener = 2.6f;

        void Start()
        {
            Play();
        }

        public override void Play()
        {
            base.Play();
            StartCoroutine(spawnAsteroidBelt());
        }

        public override void Stop()
        {
            base.Stop();
            StopCoroutine(spawnAsteroidBelt());
            Destroy(this.gameObject);
        }


        private IEnumerator spawnAsteroidBelt()
        {
            StartCoroutine(spawnAsteroid());
            yield return new WaitForSeconds(m_AsteroidSpawnTime);
            StartCoroutine(spawnAsteroidBelt());
        }

        private IEnumerator spawnAsteroid()
        {
            // get random screen vertical point to spawn asteroid and exclamation
            float screenVerticalSpawnPercentage = Random.Range(m_LowerScreenBound, m_UpperScreenBound);
            Vector3 spawnPos = Utility.ScreenUtilities.GetWSofSSPosition(1.0f, screenVerticalSpawnPercentage);

            // spawn exclamation
            Blink exclamationInstance = Instantiate(
                m_ExclamationPrefab,
                spawnPos, 
                Quaternion.identity,
                this.transform);

            // make sure blinking increases speed
            StartCoroutine(exclamationInstance.ChangeBlinkSpeed(exclamationInstance, m_ExclamationTime / 1.5f));

            // wait for certain time before spawning asteroid
            yield return new WaitForSeconds(m_ExclamationTime);

            // destroy exclamation
            Destroy(exclamationInstance.gameObject);

            // spawn asteroid after time
            Asteroid asteroidInstance = Instantiate(m_AsteroidPrefab,
                spawnPos,
                Quaternion.identity);
            asteroidInstance.AddForce(m_InitialForce * 1000.0f);
        }
    }
}
