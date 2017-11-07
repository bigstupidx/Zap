using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> m_BGSoundtracks;
        [SerializeField]
        [Range(0, 1)]
        private float m_BGVolume = 1.0f;
        private AudioClip m_CurrentBGSoundtrack;
        private int m_BGSoundtrackIndex;
        private AudioSource m_BGAudioSource;

        [SerializeField]
        private List<AudioClip> m_FGSoundtracks;
        [SerializeField]
        [Range(0, 1)]
        private float m_FGVolume = 1.0f;
        private AudioClip m_CurrentFGSoundtrack;
        private int m_FGSoundtrackIndex;
        private AudioSource m_FGAudioSource;

        [SerializeField]
        private float m_VolumeFadeInTime = 3.0f;

        void Start()
        {
            m_BGAudioSource = AudioManager.Instance.Spawn2DAudio();
            m_FGAudioSource = AudioManager.Instance.Spawn2DAudio();
            m_BGSoundtrackIndex = -1;
            m_FGSoundtrackIndex = -1;
        }

        void Update()
        {
            if (!m_BGAudioSource.isPlaying)
            {
                PlayNextBGTrack();
            }

            if (!m_FGAudioSource.isPlaying)
            {
                StartCoroutine(PlayNextFGTrack());
            }
        }

        private IEnumerator transitionFGVolumeOfCurrentTrack(float start, float end)
        {
            float currTime = 0.0f;
            while(currTime < m_VolumeFadeInTime)
            {
                currTime += Time.deltaTime;
                m_FGAudioSource.volume = Mathf.Lerp(start, end, currTime / m_VolumeFadeInTime);
                yield return null;
            }
        }

        public void RestartFGTracksFromBeginning()
        {
            m_FGSoundtrackIndex = 0;
            m_FGAudioSource.clip = m_FGSoundtracks[m_FGSoundtrackIndex];
            m_FGAudioSource.volume = m_FGVolume;
            m_FGAudioSource.Play();
        }

        public IEnumerator PlayNextFGTrack()
        {
            if(m_FGAudioSource.isPlaying)
            {
                yield return StartCoroutine(transitionFGVolumeOfCurrentTrack(1.0f, 0.0f));
            }
            m_CurrentFGSoundtrack = GetNextFGTrack();
            m_FGAudioSource.clip = m_CurrentFGSoundtrack;
            m_FGAudioSource.volume = m_FGVolume;
            m_FGAudioSource.Play();
            yield return StartCoroutine(transitionFGVolumeOfCurrentTrack(0.0f, 1.0f));
        }

        private AudioClip GetNextFGTrack()
        {
            m_FGSoundtrackIndex++;
            m_FGSoundtrackIndex = m_FGSoundtrackIndex % m_FGSoundtracks.Count;
            return m_FGSoundtracks[m_FGSoundtrackIndex];
        }

        public void PlayNextBGTrack()
        {
            m_CurrentBGSoundtrack = GetNextBGTrack();
            m_BGAudioSource.clip = m_CurrentBGSoundtrack;
            m_BGAudioSource.volume = m_BGVolume;
            m_BGAudioSource.Play();
        }

        private AudioClip GetNextBGTrack()
        {
            m_BGSoundtrackIndex++;
            m_BGSoundtrackIndex = m_BGSoundtrackIndex % m_BGSoundtracks.Count;
            return m_BGSoundtracks[m_BGSoundtrackIndex];
        }
    }
}