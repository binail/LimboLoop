using Features.Enemy.Scripts.Abstract;
using Pathfinding;
using UnityEngine;
using System.Collections;
using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;

namespace Features.Enemy.Scripts.RangeEntity
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AILerp))]
    public class Range : EnemyEntity
    {
        [Header("Enemy Properties")]
        [SerializeField] private float speed;
        [SerializeField] private float reloadTime;
        [SerializeField] private float attackDistance;
        [Header("Projectile")]
        [SerializeField] GameObject projectile;
        [SerializeField] Transform shotPoint;
        [SerializeField] private AudioAsset _shootAudio;

        private AudioHandler _audioHandler;

        private float reloadTimer;
        private bool _isRespawned;
        
        private Transform _player;
        private AILerp _ai;

        private readonly WaitForSeconds _waitForRecalculation = new(0.3f);

        public override void Construct(Transform player)
        {
            _player = player;
            _ai = GetComponent<AILerp>();
            _ai.speed = speed;
        }
        
        private void Update()
        {
            if (_isRespawned == false)
                return;

            if (reloadTimer > 0) 
                reloadTimer -= Time.deltaTime;

            var difference = _player.position - transform.position;
            var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            var distance = Vector2.Distance(_player.position, transform.position);
            
            if (distance < attackDistance && reloadTimer <= 0f)
                ProjectileShot();

            if (distance < attackDistance * 0.7f)
            {
                _ai.speed = 0f;
            }
            else
            {
                _ai.speed = speed;
            }
        }
        
        private IEnumerator RecalculatePath()
        {
            while (gameObject.activeSelf == true && _player != null)
            {
                _ai.destination = _player.position;
                
                yield return _waitForRecalculation;
            }
        }

        private void ProjectileShot()
        {
            if (_audioHandler == null || _audioHandler.IsPlaying == false)
                _audioHandler = Audio.Play(_shootAudio);
            
            Instantiate(projectile, shotPoint.position, transform.rotation);
            reloadTimer = reloadTime;
        }

        public void OnRespawned()
        {
            GetComponent<CircleCollider2D>().enabled = true;

            _isRespawned = true;
            
            StartCoroutine(RecalculatePath());
        }
    }
}
