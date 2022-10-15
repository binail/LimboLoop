using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    public class SuicideTip : MonoBehaviour
    {
        [SerializeField] private GameObject _tip;
        [SerializeField] private float _activationDistance;
        
        [SerializeField] private Color _inactive;
        [SerializeField] private Color _active;
        
        [SerializeField] private Image[] _images;
        
        private Transform _player;
        
        private static bool _isNear;

        public static bool IsNear => _isNear;

        private void Awake()
        {
            _tip.SetActive(false);
            _isNear = false;
        }

        public void Inject(Transform player)
        {
            _player = player;
        }

        private void Update()
        {
            _tip.SetActive(false);
            
            if (SuicideController.IsAllowed == false)
                return;

            _tip.SetActive(true);
            
            var distance = Vector2.Distance(transform.position, _player.position);

            if (distance > _activationDistance)
            {
                _isNear = false;

                foreach (var image in _images)
                    image.color = _inactive;
                
                return;
            }

            foreach (var image in _images)
                image.color = _active;
            
            _isNear = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, _activationDistance);
        }
    }
}