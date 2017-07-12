using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> m_Soundtracks;
    private AudioClip m_CurrentSoundtrack;

    private int m_SoundtrackIndex;

    private AudioSource m_AudioSource;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        PlayNextSoundTrack();
    }

	void Start ()
    {
        m_SoundtrackIndex = 0;
	}

    void Update()
    {
        if(!m_AudioSource.isPlaying)
        {
            PlayNextSoundTrack();
        }
    }
	
	public void PlayNextSoundTrack()
    {
        m_SoundtrackIndex++;
        m_SoundtrackIndex = m_SoundtrackIndex % m_Soundtracks.Count;
        m_CurrentSoundtrack = m_Soundtracks[m_SoundtrackIndex];
        m_AudioSource.clip = m_CurrentSoundtrack;
        m_AudioSource.Play();
    }
}
