using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

    public string m_GameSceneStr;
    public float m_SecondsUntilLoadLevel = 3.0f;

    public Animation m_ImageAnimation;
    public Animation m_TextAnimation;

    void Start()
    {
        m_ImageAnimation.Play();
        m_TextAnimation.Play();
        StartCoroutine(loadSceneAfterTime());
    }

    private IEnumerator loadSceneAfterTime()
    {
        yield return new WaitForSeconds(m_SecondsUntilLoadLevel);
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(m_GameSceneStr);
    }
}
