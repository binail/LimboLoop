using UnityEngine;

namespace Features.Escape.Scripts
{
    [DisallowMultipleComponent]
    public class EscapeCameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _targetSize;
        [SerializeField] private float _sizeSwitchSpeed = 1f;

        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;

        private bool _isReached;
        private float _totalDistance;
        
        private Transform _player;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _totalDistance = Vector2.Distance(_start.position, _end.position);
        }

        public void Inject(Transform player)
        {
            _player = player;
        }

        private void FixedUpdate()
        {
            if (_player == null)
                return;

            var currentSize = _camera.orthographicSize;

            currentSize = Mathf.Lerp(currentSize, _targetSize, _sizeSwitchSpeed * Time.deltaTime);
            _camera.orthographicSize = currentSize;


            var distance = Vector2.Distance(transform.position, _player.position);

            if (distance < 0.1f && _isReached == false)
                _isReached = true;

            if (_isReached == false)
            {
                var moveSpeed = Time.fixedDeltaTime * _speed;
                var position = transform.position;
                var targetPosition = _player.position;
                var newPosition = Vector3.Lerp(position, targetPosition, moveSpeed);
                newPosition.z = -10f;

                transform.position = newPosition;
            }
            else
            {
                var distanceToStart = Vector2.Distance(_player.position, _start.position);

                var progress = distanceToStart / _totalDistance;
                progress = Mathf.Clamp01(progress);
                var newPosition = Vector3.Lerp(_start.position, _end.position, progress);
                newPosition.z = -10f;
                transform.position = newPosition;
            }
        }
    }
}