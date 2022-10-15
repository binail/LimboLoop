using System.Collections;
using Features.Cam;
using Features.Player.Scripts;
using UnityEngine;

namespace Features.Escape.Scripts
{
    [DisallowMultipleComponent]
    public class EscapeRoot : MonoBehaviour
    {
        [SerializeField] private EscapeCameraMover _mainCamera;
        [SerializeField] private PlayerSpawner _playerSpawner;
        
        private IEnumerator Start()
        {
            var player = _playerSpawner.Spawn();

            yield return new WaitForSeconds(1.2f);
            
            _mainCamera.Inject(player);
        }
    }
}