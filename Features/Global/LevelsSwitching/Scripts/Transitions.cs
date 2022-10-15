namespace Features.Global.LevelsSwitching.Scripts
{
    public static class Transitions
    {
        private static LevelSwitcher _switcher;

        public static void Inject(LevelSwitcher switcher)
        {
            _switcher = switcher;
        }

        public static void LoadMenu()
        {
            var scene = _switcher.Config.MainMenu;

            _switcher.LoadScene(scene);
        }

        public static void LoadGame()
        {
            var scene = _switcher.Config.Game;

            _switcher.LoadScene(scene);
        }
        
        public static void LoadEscape()
        {
            var scene = _switcher.Config.Escape;

            _switcher.LoadScene(scene);
        }
        
        public static void LoadTutorial()
        {
            var scene = _switcher.Config.Tutorial;

            _switcher.LoadScene(scene);
        }
    }
}