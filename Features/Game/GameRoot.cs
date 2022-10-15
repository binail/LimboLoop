using System.Collections;
using Features.Cam;
using Features.Enemy.Scripts;
using Features.Enemy.Scripts.Waves;
using Features.Player.Scripts;
using UnityEngine;

namespace Features.Game
{
    [DisallowMultipleComponent]
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private CameraMovement _mainCamera;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemyFabric _enemyFabric;
        [SerializeField] private WavesProcessor _waves;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private SuicideTip _tip;
        
        private IEnumerator Start()
        {
            var player = _playerSpawner.Spawn();

            _healthBar.Inject(player.GetComponent<Health>());
            _enemyFabric.Inject(player);

            var suicide = player.GetComponent<SuicideController>();
            suicide.Inject(_mainCamera);
            gameObject.GetComponent<KillCounter>().Inject(suicide);
            _tip.Inject(player);
            
            _waves.Begin();

            yield return new WaitForSeconds(1.2f);
            
            _mainCamera.Inject(player);
            _mainCamera.ToFollow();
        }
    }
}