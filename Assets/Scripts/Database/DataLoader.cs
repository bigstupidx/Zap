using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameCritical;

namespace Database
{
    public class DataLoader : MonoBehaviour
    {
        private string m_UserRequestsURL = "http://localhost/zap/UserRequests.php";

        public IEnumerator AuthenticateUser(string email, string password, Action successAction, Action failAction)
        {
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.m_PARAM_REQUEST, DatabaseConstants.m_REQUEST_AUTHENTICATE_USER);
            form.AddField(DatabaseConstants.m_PARAM_EMAIL, email);
            form.AddField(DatabaseConstants.m_PARAM_PASSWORD, password);
            WWW userData = new WWW(m_UserRequestsURL, form);
            yield return userData;

            string userDataString = userData.text;
            if (userDataString == DatabaseConstants.m_RESPONSE_AUTHORIZED)
            {
                successAction();
            }
            else if (userDataString == DatabaseConstants.m_RESPONSE_UNAUTHORIZED)
            {
                failAction();
            }
            else
            {
                failAction();
            }
        }

        public IEnumerator GetHighscore(string email, Action successAction, Action failAction)
        {
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.m_PARAM_REQUEST, DatabaseConstants.m_REQUEST_GET_HIGHSCORE);
            form.AddField(DatabaseConstants.m_PARAM_EMAIL, email);
            WWW userData = new WWW(m_UserRequestsURL, form);
            yield return userData;

            string userDataString = userData.text;
            if (userDataString == DatabaseConstants.m_RESPONSE_FAILED)
            {
                if(failAction != null)
                    failAction();
            }
            else
            {
                if(successAction != null)
                    successAction();
                int highscoreFromDatabase = int.Parse(userDataString);
                SaveManager.SetHighscoreIfBetterOrDoesntExist(highscoreFromDatabase);
            }
        }
    }
}
