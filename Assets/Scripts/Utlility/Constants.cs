using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Utility
{
    public class Constants : MonoBehaviour
    {
        public static Constants Instance;

        [Tooltip("Reference to banner that contains text over unlockable button")]
        public Banner TextBannerPrefab;
        [Tooltip("Reference to banner that contains price over unlockable button")]
        public Banner PriceBannerPrefab;
        [Tooltip("Status prefab which shows on top of unlockables to show status")]
        public StatusIcon StatusPrefab;
        public Color UnlockedColor;
        public Color LockedColor;

        void Awake()
        {
            // create static instance if there is not one
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
