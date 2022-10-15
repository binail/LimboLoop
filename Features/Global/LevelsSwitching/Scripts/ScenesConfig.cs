using NaughtyAttributes;
using UnityEngine;

namespace Features.Global.LevelsSwitching.Scripts
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Transitions/Config")]
    public class ScenesConfig : ScriptableObject
    {
        [SerializeField] [Scene] private string _mainMenu;
        [SerializeField] [Scene] private string _game;
        [SerializeField] [Scene] private string _escape;
        [SerializeField] [Scene] private string _tutorial;
        
        public string MainMenu => _mainMenu;
        public string Game => _game;
        public string Escape => _escape;
        public string Tutorial => _tutorial;
    }
}