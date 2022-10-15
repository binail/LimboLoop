using System;
using System.Collections;
using Features.Abstract.Utils.Cast;
using Features.Enemy.Scripts.Waves;
using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;
using Features.Global.LevelsSwitching.Scripts;
using UnityEngine;

namespace Features.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float dashDistance;
        [SerializeField] private float timeToDash;
        [SerializeField] private float dashReloadTime;
        [SerializeField] private LayerMask mask;
        [SerializeField] private GameObject _sprites;

        [SerializeField] private AudioAsset _fallAudio;
        [SerializeField] private AudioAsset _disappearAudio;
        [SerializeField] private AudioAsset _stepAudio;
        [SerializeField] private AudioAsset _dashAudio;
        [SerializeField] private AudioAsset _pierceAudio;
        
        [SerializeField] private ParticleSystem _runParticles;
        [SerializeField] private bool _overrideMovement = false;
        [SerializeField] private Vector2 _override;
        
        private AudioHandler _stepHandler;
        
        private Rigidbody2D body;
        private Animator animator;
        private Vector3 difference;
        private Vector3 playerScale = Vector3.one;
        private float dashReloadTimer;
        private float angle;
        private bool isDashing;
        private bool _isRespawned;
        private bool _isLocked;

        private static float _dashProgress;

        public static float DashProgress => _dashProgress;
        
        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            dashReloadTimer = 0;
            isDashing = false;
            animator.enabled = false;
            _sprites.SetActive(false);
            _isLocked = false;
            Invoke(nameof(Respawn), 4f);
        }

        private void Respawn()
        {
            animator.enabled = true;
            animator.SetTrigger("Respawn");
        }

        public void OnRespawned()
        {
            _isRespawned = true;
        }

        public void OnPierce()
        {
            Audio.Play(_pierceAudio);
        }

        public void OnFall()
        {
            Audio.Play(_fallAudio);
        }

        public void OnDisappear()
        {
            Audio.Play(_disappearAudio);
        }

        public void SetLock(bool isLocked)
        {
            _isLocked = isLocked;
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.K) == true)
            {
                Transitions.LoadEscape();
                enabled = false;
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.L) == true)
            {
                WavesProcessor._waveNumber++;
        
                if (WavesProcessor._waveNumber < 9)
                    Transitions.LoadGame();
                else
                    Transitions.LoadEscape();
                
                enabled = false;
                return;
            }*/
            
            _dashProgress = Mathf.Clamp01(1f - dashReloadTimer / dashReloadTime);

            if (SuicideController.IsSuicided == true || _isLocked == true)
            {
                body.velocity = Vector2.zero;
                return;
            }
            
            if (_isRespawned == false)
                return;

            Vector2 movementVelocity;
            
            if (_overrideMovement == false)
                movementVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical")* speed);
            else
                movementVelocity = new Vector2(
                    Input.GetAxis("Horizontal") * speed + _override.x * Input.GetAxisRaw("Vertical"),
                    Input.GetAxis("Vertical") * speed + _override.y * Input.GetAxisRaw("Horizontal"));
            
            body.velocity = Vector2.ClampMagnitude(movementVelocity, speed);
            if (body.velocity == Vector2.zero)
            {
                animator.SetTrigger("Idle");
                
                if (_runParticles.isPlaying == true)
                    _runParticles.Stop();
                
            }
            else
            {
                if (_stepHandler == null || _stepHandler.IsPlaying == false)
                    _stepHandler = Audio.Play(_stepAudio);
                
                animator.SetTrigger("Run");
                
                if (_runParticles.isPlaying == false)
                    _runParticles.Play();
            }

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.localScale = playerScale;

            if(isDashing == false && dashReloadTimer > 0)
            {
                dashReloadTimer -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && isDashing == false && dashReloadTimer <= 0)
            {
                isDashing = true;
                var direction = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                StartCoroutine(Burst(direction.normalized));
            }
        }

        public IEnumerator Burst(Vector2 _direction)
        {
            Audio.Play(_dashAudio);
                        
            var _currentTime = 0f;
            var _startPosition = transform.position;

            var _purpose = Raycaster.GetPointOver(_startPosition, _direction, mask, 0.5f, dashDistance);

            if (_purpose == (Vector2)_startPosition)
            {
                _purpose = (Vector2)_startPosition + (_direction.normalized * dashDistance);
            }

            while (_currentTime < timeToDash)
            {
                _currentTime += Time.deltaTime;
                var progress = _currentTime / timeToDash;
                var position = Vector2.Lerp(_startPosition, _purpose, progress);
                transform.position = position;
                yield return null;
            }

            isDashing = false;
            dashReloadTimer = dashReloadTime;
        }
    }
}
