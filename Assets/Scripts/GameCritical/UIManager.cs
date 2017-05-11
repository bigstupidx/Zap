using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using VFX;

namespace GameCritical
{
    public class UIManager : MonoBehaviour
    {

        public InfoPanel m_InfoPanel;
        public WarpStorePanel m_WarpStorePanel;
        public MainMenuPanel m_MainMenuPanel;

        [SerializeField]
        private PopUpText m_PopUpTextPrefab;
        [SerializeField]
        private Vector3 m_PopUpTextOffset;

        // Use this for initialization
        void Awake()
        {
            if (m_InfoPanel == null)
            {
                m_InfoPanel = FindObjectOfType<InfoPanel>();
            }
            if (m_WarpStorePanel == null)
            {
                m_WarpStorePanel = FindObjectOfType<WarpStorePanel>();
            }
            if (m_MainMenuPanel == null)
            {
                m_MainMenuPanel = FindObjectOfType<MainMenuPanel>();
            }
        }

        public void SpawnPopUpText(string str, Vector3 position, Color color)
        {
            PopUpText popUpTextPrefab = (PopUpText)Instantiate(m_PopUpTextPrefab,
                position + m_PopUpTextOffset,
                Quaternion.identity);
            popUpTextPrefab.SetText(str);
            popUpTextPrefab.SetColor(color);
        }
    }
}
