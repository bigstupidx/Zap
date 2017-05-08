using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Backdrop : MonoBehaviour {

    private SpriteRenderer m_SpriteRenderer;

    private Material m_Material;

	// Use this for initialization
	void Start () {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Material = m_SpriteRenderer.material;
	}
    
    public Color GetFirstColor()
    {
        return m_Material.GetColor("_Color1");
    }

    public Color GetSecondColor()
    {
        return m_Material.GetColor("_Color2");
    }

    public void SetFirstColor(Color col)
    {
        m_Material.SetColor("_Color1", col);
	}

    public void SetSecondColor(Color col)
    {
        m_Material.SetColor("_Color2", col);
    }
}
