using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Player;

namespace Miscellaneous
{
    public class NextRowIndicator : MonoBehaviour
    {
        PlayerMovement playerMovement;
        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }

        IEnumerator showAfterTime()
        {
            yield return new WaitForSeconds(0.5f);
            if (spriteRenderer && !spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
            }
        }

        void Start()
        {
            // SUPER JANK
            StartCoroutine(showAfterTime());
            this.transform.position = ScreenUtilities.GetWSofSSPosition(0.0f, 0.0f) + new Vector3(0, 0, Camera.main.transform.position.z + 200);
            playerMovement = GameCritical.GameMaster.Instance.m_PlayerMovement;
        }

        void Update()
        {
            if(playerMovement)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, playerMovement.TargetPosition.y, this.transform.position.z), 10f * Time.deltaTime);
            }
        }
    }
}
