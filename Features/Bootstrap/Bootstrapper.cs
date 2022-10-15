using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;
using Features.Global.GamePlaySettings.Scripts;
using Features.Global.LevelsSwitching.Scripts;
using UnityEngine;

namespace Features.Bootstrap
{
    [DisallowMultipleComponent]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _audio;
        [SerializeField] private GameSettings _settings;
        [SerializeField] private LevelSwitcher _transitions;

        [SerializeField] private AudioAsset _music;
        
        [SerializeField] private bool _isTransitingToMenu = true;

        private void Awake()
        {
            _audio.Init();
            
            DontDestroyOnLoad(_audio);
            DontDestroyOnLoad(_settings);
            DontDestroyOnLoad(_transitions);
            
            Audio.Inject(_audio);
            Settings.Inject(_settings);
            Transitions.Inject(_transitions);
        }

        private void Start()
        {
            _settings.OnBootstrapped();

            Audio.Play(_music);
            
            if (_isTransitingToMenu == true)
                Transitions.LoadMenu();
        }
    }
}