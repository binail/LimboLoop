using UnityEngine;

namespace Features.Enemy.Scripts.CrabEntity
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class CrabEvents : MonoBehaviour
    {
        [SerializeField] private Crab _crab;
        
        public void OnRespawned()
        {
            _crab.OnRespawned();
        }
    }
}