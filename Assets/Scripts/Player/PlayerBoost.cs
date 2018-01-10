using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boosters;
using UI;
using GameCritical;

namespace Player
{
    public class PlayerBoost : MonoBehaviour
    {
        private Booster currentBoosterInstance;
        private Booster currentBoosterPrefab;

        [HideInInspector]
        public bool canActivate = false;

        void Start()
        {
            canActivate = true;
        }

        // sets equipped booster but doesnt activate it
        public void SetEquippedBooster(Booster booster)
        {
            currentBoosterPrefab = booster;
            BoostLoading boostLoadingUI = GameMaster.Instance.m_UIManager.m_BoostLoading;
            boostLoadingUI.SetBoostSprite(currentBoosterPrefab.m_UISprite);
        }

        // activates equipped booster
        public IEnumerator ActivateCurrentBooster()
        {
            if(currentBoosterPrefab != null && canActivate)
            {
                BoostLoading boostLoadingUI = GameMaster.Instance.m_UIManager.m_BoostLoading;
                boostLoadingUI.HideBoostImage();
                currentBoosterInstance = Instantiate(currentBoosterPrefab, this.transform);
                if(!currentBoosterInstance.shouldShowOnPlayer)
                {
                    currentBoosterInstance.spriteRenderer.enabled = false;
                }
                currentBoosterInstance.Activate();
                canActivate = false;

                // duration countdown
                yield return StartCoroutine("StartBoostDurationCountdown");

                // cooldown timer
                yield return StartCoroutine("CoolDown");

                boostLoadingUI.ShowBoostImage();
                canActivate = true;
            }
        }

        public void Reset()
        {
            // if we have a booster prefab that is equipped on player
            if(currentBoosterPrefab != null)
            {
                // stop possible coroutines that could still be running
                StopCoroutine("StartBoostDurationCountdown");
                StopCoroutine("CoolDown");
                StopCoroutine("ActivateCurrentBooster");

                // allow the boost to be reset and active
                BoostLoading boostLoadingUI = GameMaster.Instance.m_UIManager.m_BoostLoading;
                boostLoadingUI.SetPercentage(100.0f);
                canActivate = true;

                // destroy previous instance of boost is there was one
                if(currentBoosterInstance != null)
                {
                    Destroy(currentBoosterInstance.gameObject);
                }
            }
        }

        // countdown timer for when boost is over
        private IEnumerator StartBoostDurationCountdown()
        {
            BoostLoading boostLoading = GameMaster.Instance.m_UIManager.m_BoostLoading;
            if (boostLoading != null)
            {
                float currTime = currentBoosterInstance.m_CoolDownTime;
                while (currTime >= 0.0f)
                {
                    currTime -= Time.deltaTime;
                    boostLoading.SetText((currTime).ToString("F1"));
                    yield return null;
                }
            }
        }

        private IEnumerator CoolDown()
        {
            float currentCharge = 0.0f;
            while (currentCharge < currentBoosterPrefab.m_CoolDownTime)
            {
                currentCharge += Time.deltaTime;
                float percentage = currentCharge / currentBoosterPrefab.m_CoolDownTime;

                // change boost loading UI to mirror numbers
                BoostLoading boostLoading = GameMaster.Instance.m_UIManager.m_BoostLoading;
                if(boostLoading != null)
                {
                    boostLoading.SetPercentage(percentage);
                }

                yield return null;
            }

            // NOW CAN USE BOOSTER!
            canActivate = true;
            // set boost image to current booster image
            BoostLoading boostLoadingUI = GameMaster.Instance.m_UIManager.m_BoostLoading;
            boostLoadingUI.SetBoostSprite(currentBoosterPrefab.m_UISprite);
            boostLoadingUI.SetPercentage(100.0f);
        }
    }
}
