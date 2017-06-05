using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class DatabaseConstants : MonoBehaviour
    {
        public static DatabaseConstants Instance;

        public string m_UsernamePost = "usernamePost";
        public string m_PasswordPost = "passwordPost";
        public string m_EmailPost = "emailPost";

        void Awake()
        {
            // create static instance if there is not one
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
