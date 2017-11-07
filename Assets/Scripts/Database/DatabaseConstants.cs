using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public static class DatabaseConstants
    {
        // request types
        public static string m_REQUEST_AUTHENTICATE_USER = "AUTHENTICATE_USER";
        public static string m_REQUEST_CREATE_USER = "CREATE_USER";

        // parameter types
        public static string m_PARAM_USERNAME = "username";
        public static string m_PARAM_PASSWORD = "password";
        public static string m_PARAM_EMAIL = "email";
        public static string m_PARAM_REQUEST = "request";

        // response types
        public static string m_RESPONSE_AUTHORIZED = "AUTHORIZED";
        public static string m_RESPONSE_UNAUTHORIZED = "UNAUTHORIZED";
    }
}
