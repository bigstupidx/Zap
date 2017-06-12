using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class ZapBanker : MonoBehaviour
    {
        [SerializeField]
        private string m_PreString;

        private Text m_Text;

        void Awake()
        {
            m_Text = GetComponent<Text>();
        }

        public void UpdateZapsString(int zaps)
        {
            m_Text.text = m_PreString + zaps;
        }
    }
}
