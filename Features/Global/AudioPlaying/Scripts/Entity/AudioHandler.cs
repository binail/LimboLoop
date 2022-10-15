using UnityEngine;

namespace Features.Global.AudioPlaying.Scripts.Entity
{
    public class AudioHandler
    {
        public AudioHandler(AudioSource source)
        {
            _source = source;
            _clip = source.clip;
        }

        private readonly AudioSource _source;
        private readonly AudioClip _clip;

        public bool IsPlaying => _source.isPlaying;

        public void Stop()
        {
            if (IsPlaying == false)
                return;
            
            if (_source.clip != _clip)
                return;
            
            _source.Stop();
        }
    }
}