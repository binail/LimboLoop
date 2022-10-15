using System.Collections.Generic;
using Features.Enemy.Scripts.Abstract;
using UnityEngine;

namespace Features.Enemy.Scripts.Waves
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Enemies/Wave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] public int _needToKill;
        [SerializeField] private EnemyGroup[] _groups;

        public IReadOnlyList<EnemyGroup> Groups => _groups;
    }
}