using System;

using UnityEngine;


namespace ANappTestTask.Audio
{
    public delegate void OnAudioCallback(bool isActive);

    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        private AudioSource _musicSource;

        public static Action OnSetMusicState;
        public static Action OnSetSoundsState;

        public static bool IsMusicActive { get; private set; }
        public static bool IsSoundsActive { get; private set; }

        public static event Action<bool> OnChangeMusicState;
        public static event Action<bool> OnChangeSoundsState;

        private const string MusicKey = "IsMusicPlaying";
        private const string SoundsKey = "IsSoundsPlaying";

        public void Initialize()
        {
            _musicSource = GetComponent<AudioSource>();

            OnSetMusicState += SetMusicState;
            OnSetSoundsState += SetSoundsState;

            CheckCache();
        }

        private void CheckCache()
        {
            IsMusicActive = !PlayerPrefs.HasKey(MusicKey) || PlayerPrefs.GetInt(MusicKey) == 1;
            IsSoundsActive = !PlayerPrefs.HasKey(SoundsKey) || PlayerPrefs.GetInt(SoundsKey) == 1;

            if (IsMusicActive)
                _musicSource.Play();
            else
                _musicSource.Stop();

            OnChangeMusicState?.Invoke(IsMusicActive);
            OnChangeSoundsState?.Invoke(IsSoundsActive);
        }

        private void SetMusicState()
        {
            IsMusicActive = !IsMusicActive;

            if (IsMusicActive)
                _musicSource.Play();
            else
                _musicSource.Stop();

            OnChangeMusicState?.Invoke(IsMusicActive);

            PlayerPrefs.SetInt(MusicKey, (IsMusicActive) ? 1 : 0);
        }

        private void SetSoundsState()
        {
            IsSoundsActive = !IsSoundsActive;

            OnChangeSoundsState?.Invoke(IsSoundsActive);

            PlayerPrefs.SetInt(SoundsKey, (IsSoundsActive) ? 1 : 0);
        }

        private void OnDestroy()
        {
            OnChangeSoundsState = null;

            OnSetMusicState = null;
            OnSetSoundsState = null;
        }
    }
}