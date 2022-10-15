using Features.Global.AudioPlaying.Scripts.Entity;

namespace Features.Global.GamePlaySettings.Scripts
{
    public static class Settings
    {
        private static GameSettings _settings;

        public static float Music => _settings.Music;
        public static float SFX => _settings.SFX;
        public static float Sound => _settings.Sound;

        public static void Inject(GameSettings settings)
        {
            _settings = settings;
        }
        
        public static void SetAudioVolume(AudioType type, float volume)
        {
            _settings.SetVolume(type, volume);
        }
    }
}