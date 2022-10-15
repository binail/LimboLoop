using System.Collections;
using Features.Abstract.Utils.Cast;
using Features.Enemy.Scripts.Abstract;
using Pathfinding;
using UnityEngine;

namespace Features.Enemy.Scripts.CrabEntity
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AILerp))]
    public class Crab : EnemyEntity
    {
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask mask;
        [Header("Enemy Properties")]
        [SerializeField] private float speed;
        [SerializeField] private float distanceToDash;
        [SerializeField] private float timeToDash;
        [SerializeField] private float dashPreparation;

        private bool prepareToDash;
        private bool _isRespawned;
        
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
            prepareToDash = false;
        }

        private void Update()
        {
            if (_isRespawned == false)
                return;
            
            if (prepareToDash)
            {
                _ai.speed = 0f;
                return;
            }

            _ai.speed = speed;
            _ai.enabled = true;
            
            var distance = Vector2.Distance(_player.position, transform.position);
            
            if (distance < distanceToDash)
            {
                prepareToDash = true;
                _ai.enabled = false;
                
                StartCoroutine(Dash(_player.position));
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

        private IEnumerator Dash(Vector2 _player)
        {
            animator.SetTrigger("Attack_Preparation");
            yield return new WaitForSeconds(dashPreparation);

            var _currentTime = 0f;
            var _startPosition = transform.position;

            var _purpose =  _player - (Vector2)_startPosition;
            _purpose = Vector2.ClampMagnitude(_purpose, 1);
            _purpose = (Vector2)_startPosition + _purpose * distanceToDash;


            animator.SetTrigger("Attack_Jump");
            while (_currentTime < timeToDash)
            {
                _currentTime += Time.deltaTime;
                var progress = _currentTime / timeToDash;
                var position = Vector2.Lerp(_startPosition, _purpose, progress);
                transform.position = position;
                yield return null;
            }

            prepareToDash = false;
            animator.SetTrigger("Run");
        }

        public void OnRespawned()
        {
            GetComponent<CircleCollider2D>().enabled = true;
            _isRespawned = true;
            animator.SetTrigger("Run");
            StartCoroutine(RecalculatePath());
        }
    }
}
