using System;
using UnityEngine;

namespace Features.Enemy.Scripts.Abstract
{
    public abstract class EnemyEntity : MonoBehaviour
    {
        public static int Count;
        
        public abstract void Construct(Transform player);

        private void Awake()
        {
            Count++;
        }

        private void OnDestroy()
        {
            Count--;
        }
    }
}