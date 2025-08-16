using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
namespace UI
{

    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        [Header("Setup")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip[] musicTracks;
        [SerializeField] private float fadeDuration = 1.5f;

        private int currentTrackIndex = -1;
        private Tween fadeTween;

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

            musicSource.loop = true;
            PlayTrackByIndex(0);
        }

        public void PlayTrackByIndex(int index)
        {
            if (index < 0 || index >= musicTracks.Length) return;
            if (index == currentTrackIndex) return;

            currentTrackIndex = index;
            AudioClip nextClip = musicTracks[index];
            FadeToTrack(nextClip);
        }

        

        private void FadeToTrack(AudioClip newClip)
        {
            // Stop any existing fade
            fadeTween?.Kill();

            // Fade out
            fadeTween = musicSource.DOFade(0f, fadeDuration / 2f).OnComplete(() =>
            {
                musicSource.clip = newClip;
                musicSource.Play();

                // Fade in
                fadeTween = musicSource.DOFade(1f, fadeDuration / 2f);
            });
        }
    }

}