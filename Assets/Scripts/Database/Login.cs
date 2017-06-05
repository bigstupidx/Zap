using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class Login : MonoBehaviour
    {
        [SerializeField]
        private string m_Username;
        [SerializeField]
        private string m_Password;

        private string m_LoginURL = "http://localhost/Zap/Login.php";

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(LoginUser(m_Username, m_Password));
            }
        }

        /*public IEnumerator CreateUser(string username, string password)
        {

        }*/

        public IEnumerator LoginUser(string username, string password)
        {
            WWWForm form = new WWWForm();
            form.AddField(DatabaseConstants.Instance.m_UsernamePost, username);
            form.AddField(DatabaseConstants.Instance.m_PasswordPost, password);

            WWW www = new WWW(m_LoginURL, form);

            yield return www; // wait for web request response

            if(www.error != null)
            {
                // failure!
                Debug.Log("Failed login request");
                yield return null;
            }

            // success!
            Debug.Log(www.text);
        }
    }
}
