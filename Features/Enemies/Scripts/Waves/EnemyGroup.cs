using System;
using System.Collections.Generic;
using Features.Enemy.Scripts.Abstract;
using UnityEngine;

namespace Features.Enemy.Scripts.Waves
{
    [Serializable]
    public class EnemyGroup
    {
        [SerializeField] private int _crabs;
        [SerializeField] private int _wardens;
        [SerializeField] private int _ranges;

        [SerializeField] private float _delayBefore;
        [SerializeField] private float _delayAfter;

        public int Crabs => _crabs;
        public int Wardens => _wardens;
        public int Ranges => _ranges;

        public float DelayBefore => _delayBefore;
        public float DelayAfter => _delayAfter;

        public IReadOnlyList<EnemyType> GetEnemies()
        {
            var enemies = new List<EnemyType>();

            for (var i = 0; i < _crabs; i++)
                enemies.Add(EnemyType.Crab);
            
            for (var i = 0; i < _wardens; i++)
                enemies.Add(EnemyType.Warden);

            for (var i = 0; i < _ranges; i++)
                enemies.Add(EnemyType.Range);

            return enemies;
        }
    }
}