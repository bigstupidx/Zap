using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Database
{
    public class DataInserter : MonoBehaviour
    {
        public string m_InputUserName;
        public string m_InputPassword;
        public string m_InputEmail;

        private string m_UserRequestsURL = "http://localhost/zap/UserRequests.php";

        public IEnumerator CreateUser(string username, string password, string email, 
            Action successAction, Action failedAction, Action alreadyExistsAction)
        {
            // send create user request
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.m_PARAM_REQUEST, DatabaseConstants.m_REQUEST_CREATE_USER);
            form.AddField(DatabaseConstants.m_PARAM_USERNAME, username);
            form.AddField(DatabaseConstants.m_PARAM_PASSWORD, password);
            form.AddField(DatabaseConstants.m_PARAM_EMAIL, email);
            WWW responseMessage = new WWW(m_UserRequestsURL, form);
            yield return responseMessage;

            // get response
            string responseString = responseMessage.text;
            if (responseString == DatabaseConstants.m_RESPONSE_ALREADY_EXISTS)
            {
                alreadyExistsAction();
            }
            else if(responseString == DatabaseConstants.m_RESPONSE_AUTHORIZED)
            {
                successAction();
            }
            else
            {
                failedAction();
            }
        }

        public IEnumerator SetHighScore(string email, int localScore, Action successAction, Action failedAction)
        {
            // send create user request
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.m_PARAM_REQUEST, DatabaseConstants.m_REQUEST_SET_HIGHSCORE);
            form.AddField(DatabaseConstants.m_PARAM_EMAIL, email);
            form.AddField(DatabaseConstants.m_PARAM_SCORE, localScore);
            WWW responseMessage = new WWW(m_UserRequestsURL, form);
            yield return responseMessage;

            // get response
            string responseString = responseMessage.text;
            if (responseString == DatabaseConstants.m_RESPONSE_SUCCESS)
            {
                if(successAction != null)
                    successAction();
            }
            else
            {
                if (failedAction != null)
                    failedAction();
            }
        }
    }
}
