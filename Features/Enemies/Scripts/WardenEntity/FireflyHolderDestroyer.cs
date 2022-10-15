using UnityEngine;

namespace Features.Enemy.Scripts.WardenEntity
{
    public class FireflyHolderDestroyer : MonoBehaviour
    {
        [SerializeField] private float lifetime;

        private bool isActivated;

        private void Start()
        {
            isActivated = false;
        }

        private void Update()
        {
            if (isActivated)
            {
                return;
            }

            lifetime -= Time.deltaTime;

            if(lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void Activate()
        {
            isActivated = true;
        }

        public void DestroyFirefly()
        {
            Destroy(gameObject);
        }
    }
}
