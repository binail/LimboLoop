using Features.Player.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts.WardenEntity
{
    public class Firefly : MonoBehaviour
    {
        [SerializeField] private FireflyHolderDestroyer fireflyHolderDestroyer;
        [SerializeField] private GameObject particle;
        [SerializeField] private float speed;
        [SerializeField] private float materializationTime;
        [SerializeField] private float damage;
        [SerializeField] private float lifetime;

        private Transform purpose;
        private bool isFly;

        private void Start()
        {
            isFly = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        private void Update()
        {
            if(materializationTime > 0)
            {
                materializationTime -= Time.deltaTime;
                return;
            }

            gameObject.GetComponent<CircleCollider2D>().enabled = true;

            if (isFly == false) return;

            if (lifetime > 0)
            {
                lifetime -= Time.deltaTime;
            }
            else if (lifetime <= 0)
            {
                fireflyHolderDestroyer.DestroyFirefly();
                Instantiate(particle, transform.position, transform.rotation);
                Destroy(gameObject);   
            }

            var _step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, purpose.position, _step);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                fireflyHolderDestroyer.DestroyFirefly();
                Destroy(gameObject);
            }
        }

        public void PlayerDetected(Transform _player)
        {
            isFly = true;
            purpose = _player;
            fireflyHolderDestroyer.Activate();
        }
    }
}
