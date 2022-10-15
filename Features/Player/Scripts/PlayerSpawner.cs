using UnityEngine;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _prefab;
        [SerializeField] private Transform _point;

        public Transform Spawn()
        {
            var player = Instantiate(_prefab, _point.position, Quaternion.identity);

            return player.transform;
        }
    }
}