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
        public static string m_REQUEST_SET_HIGHSCORE = "SET_HIGHSCORE";

        // parameter types
        public static string m_PARAM_USERNAME = "username";
        public static string m_PARAM_PASSWORD = "password";
        public static string m_PARAM_EMAIL = "email";
        public static string m_PARAM_REQUEST = "request";
        public static string m_PARAM_SCORE = "score";

        // response types
        public static string m_RESPONSE_AUTHORIZED = "AUTHORIZED";
        public static string m_RESPONSE_UNAUTHORIZED = "UNAUTHORIZED";
        public static string m_RESPONSE_ALREADY_EXISTS = "ALREADY_EXISTS";
        public static string m_RESPONSE_SUCCESS = "SUCCESS";
        public static string m_RESPONSE_FAILED = "FAILED";
    }
}
