using System;
using System.Collections.Generic;
using System.Linq;
using Features.Enemy.Scripts.Abstract;
using Features.Enemy.Scripts.CrabEntity;
using Features.Enemy.Scripts.WardenEntity;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = Features.Enemy.Scripts.RangeEntity.Range;

namespace Features.Enemy.Scripts
{
    [DisallowMultipleComponent]
    public class EnemyFabric : MonoBehaviour
    {
        [SerializeField] private Warden _warden;
        [SerializeField] private Crab _crab;
        [SerializeField] private Range _range;

        [SerializeField] private Transform[] _points;
        
        private Transform _player;
        
        public void Inject(Transform player)
        {
            _player = player;
        }

        public void Spawn(IReadOnlyList<EnemyType> enemies)
        {
            var points = _points.ToList();

            foreach (var enemy in enemies)
            {
                var random = Random.Range(0, points.Count);
                var point = points[random];
                points.RemoveAt(random);
                InstantiateEnemy(enemy, point);
            }
        }

        private void InstantiateEnemy(EnemyType type, Transform point)
        {
            EnemyEntity entity = type switch
            {
                EnemyType.Crab => _crab,
                EnemyType.Warden => _warden,
                EnemyType.Range => _range,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var enemy = Instantiate(entity, point.position, Quaternion.identity);
            enemy.Construct(_player);
        }
    }
}