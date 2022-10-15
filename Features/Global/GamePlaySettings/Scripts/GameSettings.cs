using System;
using UnityEngine;
using UnityEngine.Audio;
using AudioType = Features.Global.AudioPlaying.Scripts.Entity.AudioType;

namespace Features.Global.GamePlaySettings.Scripts
{
    [DisallowMultipleComponent]
    public class GameSettings : MonoBehaviour
    {
        private const float _minVolume = -80f;
        private const float _maxVolume = 20f;

        private const string _soundParam = "Volume_Sound";
        private const string _sfxParam = "Volume_SFX";
        private const string _musicParam = "Volume_Music";
        
        [SerializeField] private float _soundVolume = 0.5f;
        [SerializeField] private float _sfxVolume = 0.5f;
        [SerializeField] private float _musicVolume = 0.5f;

        [SerializeField] private AudioMixer _mixer;

        public float Sound => _soundVolume;
        public float SFX => _sfxVolume;
        public float Music => _musicVolume;

        public void OnBootstrapped()
        {
            SetVolume(AudioType.Sound, _soundVolume);
            SetVolume(AudioType.SFX, _sfxVolume);
            SetVolume(AudioType.Music, _musicVolume);
        }

        public void SetVolume(AudioType type, float volume)
        {
            volume = Mathf.Clamp01(volume);

            const float delta = _maxVolume - _minVolume;
            var convertedVolume = _minVolume + delta * volume;
            
            switch (type)
            {
                case AudioType.Sound:
                    _soundVolume = volume;
                    _mixer.SetFloat(_soundParam, convertedVolume);
                    break;
                case AudioType.SFX:
                    _sfxVolume = volume;
                    _mixer.SetFloat(_sfxParam, convertedVolume);
                    break;
                case AudioType.Music:
                    _musicVolume = volume;
                    _mixer.SetFloat(_musicParam, convertedVolume);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}