using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class DataInserter : MonoBehaviour
    {
        public string m_InputUserName;
        public string m_InputPassword;
        public string m_InputEmail;

        private string m_CreateUserURL = "http://localhost/Zap/InsertUser.php";

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateUser(m_InputUserName, m_InputPassword, m_InputEmail);
            }
        }

        public void CreateUser(string username, string password, string email)
        {
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.Instance.m_UsernamePost, username);
            form.AddField(DatabaseConstants.Instance.m_EmailPost, email);
            form.AddField(DatabaseConstants.Instance.m_PasswordPost, password);

            WWW www = new WWW(m_CreateUserURL, form);
        }
    }
}
