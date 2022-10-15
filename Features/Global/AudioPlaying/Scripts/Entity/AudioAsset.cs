using UnityEngine;

namespace Features.Global.AudioPlaying.Scripts.Entity
{
    [CreateAssetMenu(fileName = "Audio", menuName = "Audio/Asset")]
    public class AudioAsset : ScriptableObject
    {
        [SerializeField][Range(0f, 1f)]
        private float _volume;

        [SerializeField] private AudioType _type;

        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private bool _isLooping = false;

        public float Volume => _volume;
        public AudioType Type => _type;
        public bool IsLooping => _isLooping;

        public AudioClip GetClip()
        {
            var random = Random.Range(0, _clips.Length);

            return _clips[random];
        }
    }
}