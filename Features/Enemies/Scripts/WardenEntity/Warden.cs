using System.Collections;
using Features.Enemy.Scripts.Abstract;
using Pathfinding;
using UnityEngine;

namespace Features.Enemy.Scripts.WardenEntity
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AILerp))]
    public class Warden : EnemyEntity
    {
        [SerializeField] private Animator animator;

        [Header("Enemy Properties")] 
        [SerializeField] private float speed;

        [SerializeField] private float reloadTime;
        [SerializeField] private float casteDistance;

        [Header("Firefly")] 
        [SerializeField] GameObject firefly;

        private float reloadTimer;
        private bool isCasting;
        private bool _isRespawned;
        private Vector2 _attackTarget;

        private Transform _player;
        private AILerp _ai;

        private readonly WaitForSeconds _waitForRecalculation = new(0.3f);

        public override void Construct(Transform player)
        {
            _player = player;
            _ai = GetComponent<AILerp>();
        }

        private void Start()
        {
            isCasting = false;
            reloadTimer = reloadTime;
        }
        
        private void Update()
        {
            if (_isRespawned == false)
                return;
            
            if (isCasting)
            {
                _ai.speed = 0f;
                return;
            }

            _ai.speed = speed;

            if (reloadTimer > 0) 
                reloadTimer -= Time.deltaTime;

            var distance = Vector2.Distance(_player.position, transform.position);
            
            if (distance < casteDistance && reloadTimer <= 0f)
            {
                isCasting = true;
                _attackTarget = _player.position;
                animator.SetTrigger("Attack");
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

        public void SpawnFirefly()
        {
            Instantiate(firefly, _attackTarget, firefly.transform.rotation);

            isCasting = false;
            _ai.speed = speed;
            reloadTimer = reloadTime;

            animator.SetTrigger("Run");
        }

        public void OnRespawned()
        {
            GetComponent<BoxCollider2D>().enabled = true;

            _isRespawned = true;
            animator.SetTrigger("Run");
            StartCoroutine(RecalculatePath());
        }
    }
}