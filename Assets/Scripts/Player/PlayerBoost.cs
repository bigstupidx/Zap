using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boosters;

namespace Player
{
    public class PlayerBoost : MonoBehaviour
    {
        private Booster currentBooster;
        
        public void SetCurrentBooster(Booster booster)
        {
            currentBooster = booster;
        }
    }
}
