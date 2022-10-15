using UnityEngine;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerKatanaProgressionUpdater : MonoBehaviour
    {
        private const string _parameterName = "_Progress";
        private readonly int _parameterId = Shader.PropertyToID(_parameterName);

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sharedMaterial.SetFloat(_parameterId, 0f);
        }

        private void OnEnable()
        {
            KillCounter.EnemyKilled += OnEnemyKilled;
        }
        
        private void OnDisable()
        {
            KillCounter.EnemyKilled -= OnEnemyKilled;
        }

        private void OnEnemyKilled(float progress)
        {
            _spriteRenderer.sharedMaterial.SetFloat(_parameterId, progress);
        }
    }
}