using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testwave : MonoBehaviour {

    private Material m_Material;
    [SerializeField]
    private float m_Intensity;

	void Start () {
        m_Material = GetComponent<SpriteRenderer>().material;
        m_Material.SetFloat("_Intensity", m_Intensity);
    }
}
