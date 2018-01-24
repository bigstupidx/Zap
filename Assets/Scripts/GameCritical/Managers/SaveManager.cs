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
        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
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

        public static bool SetHighscoreIfBetterOrDoesntExist(int score)
        {
            if (SaveManager.IsStringStored(Database.DatabaseConstants.m_HIGHSCORE))
            {
                if (score > SaveManager.GetInt(Database.DatabaseConstants.m_HIGHSCORE))
                {
                    SaveManager.SetInt(Database.DatabaseConstants.m_HIGHSCORE, score);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                SaveManager.SetInt(Database.DatabaseConstants.m_HIGHSCORE, score);
                return true;
            }
        }
    }
}
