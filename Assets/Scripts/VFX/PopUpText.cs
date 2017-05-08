using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    [RequireComponent(typeof(TextMesh))]
    public class PopUpText : MonoBehaviour
    {

        private TextMesh m_Text;

        void Awake()
        {
            m_Text = GetComponent<TextMesh>();
        }

        // Use this for initialization
        void Start()
        {

        }

        public void SetColor(Color col)
        {
            if (m_Text != null)
                m_Text.color = col;
        }

        public void SetText(string str)
        {
            if (m_Text != null)
                m_Text.text = str;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
