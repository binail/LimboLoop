using Features.Global.AudioPlaying.Scripts.Entity;

namespace Features.Global.AudioPlaying.Scripts
{
    public static class Audio
    {
        private static AudioPlayer _player;
        
        public static void Inject(AudioPlayer player)
        {
            _player = player;
        }

        public static AudioHandler Play(AudioAsset asset)
        {
            return _player.Play(asset);
        }
    }
}