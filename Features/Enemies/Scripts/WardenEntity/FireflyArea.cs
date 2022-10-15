using UnityEngine;

namespace Features.Enemy.Scripts.WardenEntity
{
    public class FireflyArea : MonoBehaviour
    {
        [SerializeField] private Firefly firefly;
        [SerializeField] private float materializationTime;
        [SerializeField] private float timeToDestroy;

        private SpriteRenderer spriteRenderer;
        private bool isActivate;
        private float destroyTimer;
        private float materializationTimer;

        private void Start()
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            isActivate = false;
            destroyTimer = timeToDestroy;
            materializationTimer = 0;
        }

        private void Update()
        {
            if (isActivate)
            {
                destroyTimer -= Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, destroyTimer/timeToDestroy);

                if (destroyTimer <= 0)
                {
                    Destroy(gameObject);
                }

                return;
            }

            if (materializationTimer < materializationTime)
            {
                materializationTimer += Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, materializationTimer / materializationTime);
            }
            else if (materializationTimer >= materializationTime)
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player");
                firefly.PlayerDetected(collision.gameObject.GetComponent<Transform>());
                isActivate = true;
            }
            else
            {
                Debug.Log("Not player");
            } 
        }
    }
}
