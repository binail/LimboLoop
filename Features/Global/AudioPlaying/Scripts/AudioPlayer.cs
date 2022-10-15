using System;
using System.Collections.Generic;
using Features.Global.AudioPlaying.Scripts.Entity;
using UnityEngine;
using AudioType = Features.Global.AudioPlaying.Scripts.Entity.AudioType;

namespace Features.Global.AudioPlaying.Scripts
{
    [DisallowMultipleComponent]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _soundSources;
        [SerializeField] private AudioSource[] _sfxSources;
        [SerializeField] private AudioSource[] _musicSources;

        private readonly Queue<AudioSource> _sound = new();
        private readonly Queue<AudioSource> _sfx = new();
        private readonly Queue<AudioSource> _music = new();

        public void Init()
        {
            foreach (var source in _soundSources)
                _sound.Enqueue(source);
            
            foreach (var source in _sfxSources)
                _sfx.Enqueue(source);
            
            foreach (var source in _musicSources)
                _music.Enqueue(source);
        }

        public AudioHandler Play(AudioAsset asset)
        {
            var source = GetSource(asset.Type);

            source.clip = asset.GetClip();
            source.volume = asset.Volume;
            source.loop = asset.IsLooping;
            source.Play();

            var handler = new AudioHandler(source);

            return handler;
        }

        private AudioSource GetSource(AudioType type)
        {
            var queue = type switch
            {
                AudioType.Sound => _sound,
                AudioType.SFX => _sfx,
                AudioType.Music => _music,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var source = queue.Dequeue();

            if (source.isPlaying == true)
                Debug.Log($"Not enough {type} sources. Picking active");
            
            queue.Enqueue(source);

            return source;
        }
    }
}