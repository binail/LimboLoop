using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image[] currenthealth;

        private Health playerHealth;

        private void Update()
        {
            if (playerHealth == null) return;

            if (playerHealth.currentHealth < currenthealth.Length)
            {
                for (int i =currenthealth.Length - ((int)playerHealth.currentHealth); i > 0; i--)
                {
                    currenthealth[currenthealth.Length - i].enabled = false;
                }
            }
        }

        public void Inject(Health _playerHealth)
        {
            playerHealth = _playerHealth;
        }
    }
}
