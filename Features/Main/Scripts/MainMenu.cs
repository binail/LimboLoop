using Features.Enemy.Scripts.Waves;
using Features.Global.LevelsSwitching.Scripts;
using UnityEngine;

namespace Features.Main.Scripts
{
    [DisallowMultipleComponent]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _tutorial;

        private void Awake()
        {
            _settings.SetActive(false);
            _tutorial.SetActive(false);
        }

        public void OnPlayClicked()
        {
            WavesProcessor._waveNumber = 0;
            Transitions.LoadTutorial();
        }

        public void OnSettingsClicked()
        {
            _tutorial.SetActive(false);
            _settings.SetActive(!_settings.activeSelf);
        }
        
        public void OnTutorialClicked()
        {
            _settings.SetActive(false);
            _tutorial.SetActive(!_tutorial.activeSelf);
        }
    }
}