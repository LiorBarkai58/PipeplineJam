using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace.Audio
{

    public class SfxManager : MonoBehaviour
    {
        public static SfxManager Instance;

        [SerializeField] private int sourcePoolSize = 10;
        [SerializeField] private AudioSource audioSourcePrefab;

        private List<AudioSource> audioSources;

        private void Awake()
        {
            // Singleton setup
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitAudioSources();
        }

        private void InitAudioSources()
        {
            audioSources = new List<AudioSource>(sourcePoolSize);

            for (int i = 0; i < sourcePoolSize; i++)
            {
                AudioSource newSource = Instantiate(audioSourcePrefab, transform);
                newSource.playOnAwake = false;
                audioSources.Add(newSource);
            }
        }

        public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            AudioSource source = GetAvailableSource();

            if (source != null)
            {
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
            }
        }

        private AudioSource GetAvailableSource()
        {
            foreach (var source in audioSources)
            {
                if (!source.isPlaying)
                    return source;
            }

            // If all sources are busy, return the one that's closest to finishing
            AudioSource fallback = audioSources[0];
            float minTimeLeft = fallback.clip ? fallback.clip.length - fallback.time : float.MaxValue;

            foreach (var source in audioSources)
            {
                float timeLeft = source.clip ? source.clip.length - source.time : float.MaxValue;
                if (timeLeft < minTimeLeft)
                {
                    fallback = source;
                    minTimeLeft = timeLeft;
                }
            }

            return fallback;
        }
    }

}