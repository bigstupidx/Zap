using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    [SerializeField]
    public AudioSource m_AudioSourcePrefab;

	void Awake ()
    {
        Instance = this;
	}

    void Start ()
    {
        if(m_AudioSourcePrefab == null)
        {
            Debug.LogError("Audio source prefab is null");
        }
    }
	
	
    public AudioSource Spawn2DAudio(AudioClip audioClip = null)
    {
        if(m_AudioSourcePrefab == null)
        {
            return null;
        }

        AudioSource audioSource = Instantiate(m_AudioSourcePrefab, this.transform);

        if(audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        return audioSource;
    }
}
