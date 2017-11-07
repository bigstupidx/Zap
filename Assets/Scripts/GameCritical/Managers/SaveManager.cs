using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public static class SaveManager
    {
        public static bool IsStringStored(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public static void RemoveKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
