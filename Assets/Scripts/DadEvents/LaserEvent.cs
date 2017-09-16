using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace DadEvents
{
    public class LaserEvent : DadEvent
    {
        [SerializeField]
        private Laser m_LaserPrefab;

        [SerializeField]
        private float m_SpawnTime;

        public override void Play()
        {
            base.Play();
            StartCoroutine("spawnLaser");
        }

        public override void Stop()
        {
            base.Stop();
            StopCoroutine("spawnLaser");
        }

        private IEnumerator spawnLaser()
        {
            ZapManager zapManager = GameMaster.Instance.m_ZapManager;
            if(zapManager)
            {
                ZapGrid zapGrid = zapManager.GetZapGrid();
                int numRows = zapGrid.GetNumRows();
                int numCols = zapGrid.GetNumCols(0);

                // get random row to spawn lasers on
                int randomRow = Random.Range(0, numRows);
                Zap zapInRow = zapGrid.GetZap(randomRow, 0);

                // spawn on left or right side of the screen
                int randomSide = Random.Range(0, 2);
                bool isOnRightSide = (randomSide == 1) ? true : false;
                Vector3 spawnPos = Vector3.zero;

                // spawn the laser
                Laser laserInstance = Instantiate(m_LaserPrefab, this.transform);

                // make changes to lasers position based on if it is on left or right side of the screen.
                if (randomSide == 0) // spawn on left side of screen
                {
                    spawnPos = Utility.ScreenUtilities.GetWSofSSPosition(0.0f, 0.0f);
                }
                else // spawn on right side of screen
                {
                    spawnPos = Utility.ScreenUtilities.GetWSofSSPosition(1.0f, 0.0f);
                }

                // make sure the laser is spawned at the same y position as the zap row.
                spawnPos.y = zapInRow.GetOffsetPosition().y;
                laserInstance.SetPositionLaserPost(spawnPos, isOnRightSide);
            }

            yield return new WaitForSeconds(m_SpawnTime);
            StartCoroutine(spawnLaser());
        }
    }
}
