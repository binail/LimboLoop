using Features.Player.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] float damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
