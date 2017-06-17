using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utlities
{
    public static class ScreenUtilities
    {
        // gets world space of screen space location
        public static Vector3 GetWSofSSPosition(float xPercentage, float yPercentage)
        {
           return Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Camera.main.pixelWidth * Mathf.Clamp(xPercentage, 0.0f, 1.0f),
                    Camera.main.pixelHeight * Mathf.Clamp(yPercentage, 0.0f, 1.0f),
                    0));
        }
    }
}
