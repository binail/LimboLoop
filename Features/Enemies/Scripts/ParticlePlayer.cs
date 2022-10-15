using UnityEngine;

namespace Features.Enemy.Scripts
{
    [DisallowMultipleComponent]
    public class ParticlePlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;

        public void Play()
        {
            _particles.Play();
        }
    }
}