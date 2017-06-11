using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour {
    [SerializeField]
    private string m_MainScene;

	void Start ()
    {
        SceneManager.LoadScene(m_MainScene);
	}
}
