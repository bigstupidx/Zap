using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Database
{
    public class DataLoader : MonoBehaviour
    {
        private string m_GetUserURL = "http://localhost/zap/UserRequests.php";

        public IEnumerator AuthenticateUser(string username, string password, Action successAction, Action failAction)
        {
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.m_PARAM_REQUEST, DatabaseConstants.m_REQUEST_AUTHENTICATE_USER);
            form.AddField(DatabaseConstants.m_PARAM_USERNAME, username);
            form.AddField(DatabaseConstants.m_PARAM_PASSWORD, password);
            WWW userData = new WWW(m_GetUserURL, form);
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
    }
}
