using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WarpStorePanel : MonoBehaviour
    {
        void Start()
        {
            Hide();
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}
