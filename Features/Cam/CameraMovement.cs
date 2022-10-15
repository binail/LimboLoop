using UnityEngine;

namespace Features.Cam
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _followSize;
        [SerializeField] private float _suicideSize;
        [SerializeField] private float _sizeSwitchSpeed = 1f;
        
        private float _targetSize;
        
        private Transform _player;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
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

            currentSize = Mathf.Lerp(currentSize, _targetSize, _sizeSwitchSpeed * Time.fixedDeltaTime);
            _camera.orthographicSize = currentSize;

            var moveSpeed = Time.fixedDeltaTime * _speed;
            var position = transform.position;
            var targetPosition = _player.position;
            var newPosition = Vector3.Lerp(position, targetPosition, moveSpeed);
            newPosition.z = -10f;

            transform.position = newPosition;
        }

        public void ToFollow()
        {
            _targetSize = _followSize;
        }

        public void ToSuicide()
        {
            _targetSize = _suicideSize;
        }
    }
}
