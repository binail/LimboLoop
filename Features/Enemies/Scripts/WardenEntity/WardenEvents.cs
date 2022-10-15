using UnityEngine;

namespace Features.Enemy.Scripts.WardenEntity
{
    public class WardenEvents : MonoBehaviour
    {
        [SerializeField] private Warden _warden;

        public void OnRespawned()
        {
            _warden.OnRespawned();
        }
        
        public void EndOfAttack()
        {
            _warden.SpawnFirefly();
        }
    }
}
