using System;
using System.Collections;
using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;
using UnityEngine;

namespace Features.Player.Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private GameObject _damageParticles;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private float startingHealth;
        [SerializeField] private int pointsForKill;
        [SerializeField] private AudioAsset _damageAudio;
        [SerializeField] private AudioAsset _deathAudio;

        private AudioHandler _audioHandler;

        public float currentHealth { get; private set; }
        private SpriteRenderer spriteRend;
        private bool invulnerability = false;

        private PlayerMovement _movement;
        
        public static event Action Died;


        void Awake()
        {
            currentHealth = startingHealth;
            _movement = GetComponent<PlayerMovement>();
        }
    
        public void TakeDamage(float _damage)
        {
            if (gameObject.tag == "Player" && invulnerability) return;

            if (currentHealth > 0)
            {
                if (_audioHandler == null || _audioHandler.IsPlaying == false)
                    _audioHandler = Audio.Play(_damageAudio);
                
                currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

                if (currentHealth > 0)
                {
                    Instantiate(_damageParticles, transform.position, transform.rotation);
                }
            }

            if (currentHealth == 0)
            {
                if (_audioHandler == null || _audioHandler.IsPlaying == false && gameObject.tag != "Player")
                    _audioHandler = Audio.Play(_deathAudio);
                
                Death();
            }

            if (gameObject.tag == "Player")
            {
                StartCoroutine(Invulnerability());
            }
        }

        private void Death()
        {
            if (gameObject.tag == "Enemy")
            {
                Instantiate(_deathParticles, transform.position, transform.rotation);
                KillCounter.GetKillPoints(pointsForKill);
                Destroy(gameObject);
            }
            else
            {
                _movement.SetLock(true);
                Died?.Invoke();
            }
        }

        private IEnumerator Invulnerability()
        {
            invulnerability = true;

            yield return new WaitForSeconds(0.1f);

            invulnerability = false;
        }
    }
}
