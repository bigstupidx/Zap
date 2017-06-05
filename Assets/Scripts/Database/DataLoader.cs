using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class DataLoader : MonoBehaviour
    {

        private string[] highscores;
        private string m_GetHighscoresURL = "http://localhost/Zap/Highscores.php";

        // Use this for initialization
        IEnumerator Start()
        {
            WWW highscoreData = new WWW(m_GetHighscoresURL);
            yield return highscoreData; // waits for data to be obtained
            string highscoreDataStr = highscoreData.text;
            highscores = highscoreDataStr.Split(';');
            print(GetHighScore(highscores[0], "highscore:"));
        }

        string GetHighScore(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);

            // remove excess fat from data if need be
            int startCuttOffIndex = value.IndexOf("|");
            if (startCuttOffIndex >= 0)
            {
                value = value.Remove(startCuttOffIndex);
            }

            return value;
        }
    }
}
