using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Obstacles
{
    public class SpikeBall : Obstacle
    {
        [SerializeField]
        private float m_PlayerSlowTime = 2.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void ApplyObstacleEffect()
        {
            StartCoroutine(ApplySlowForTime(2.0f));
        }

        private IEnumerator ApplySlowForTime(float time)
        {
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(0.1f, false);
            yield return new WaitForSeconds(time);
            GameMaster.Instance.m_PlayerMovement.SetSpeedMultiplier(1.0f, true);
        }
    }
}
