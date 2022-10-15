using Features.Player.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts.RangeEntity
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;
        [SerializeField] private float damage;

        private void Update()
        {
            lifetime -= Time.deltaTime;
            if(lifetime <= 0)
            {
                Destroy(gameObject);
            }

            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        
        }
    }
}
