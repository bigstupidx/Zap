using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WarpStoreButton : MonoBehaviour {

        [Tooltip("Main image to display")]
        public Sprite m_Main;
        [Tooltip("Reference to image container that will change its image")]
        public Image m_Image;
        public string m_Text;
        public Banner m_Banner;
        public Transform m_BannerSpawn;

	    void Start()
        {
            if(m_Image != null)
            {
                if(m_Main != null)
                {
                    m_Image.sprite = m_Main;
                }
            }
            if(m_Banner != null)
            {
                m_Banner = Instantiate(
                    m_Banner, 
                    m_BannerSpawn.position, 
                    m_BannerSpawn.rotation, 
                    this.transform);
                m_Banner.SetText(m_Text);
            }
        }
    }
}