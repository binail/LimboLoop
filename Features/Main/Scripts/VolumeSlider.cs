using System;
using Features.Global.GamePlaySettings.Scripts;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Features.Global.AudioPlaying.Scripts.Entity.AudioType;

namespace Features.Main.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioType _type;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();

            _slider.value = _type switch
            {
                AudioType.Sound => Settings.Sound,
                AudioType.SFX => Settings.SFX,
                AudioType.Music => Settings.Music,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float slider)
        {
            Settings.SetAudioVolume(_type, slider);
        }
    }
}