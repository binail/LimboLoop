using UnityEngine;

namespace Features.Sword.Scripts
{
    public class SwordMoving : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;

        private float angle;
        private Vector3 difference;
        private Vector3 swordScale = Vector3.one;

        public bool isAttack = false;

        void Update()
        {
            var _playerDirection = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;

            if (rigidbody.velocity != Vector2.zero)
            {
                if (_playerDirection < 90 && _playerDirection > -90)
                {
                    swordScale.x = -1f;
                    transform.localScale = swordScale;
                }
                else if (_playerDirection > 90 || _playerDirection < -90)
                {
                    swordScale.x = 1f;
                    transform.localScale = swordScale;
                }
            }

            if (isAttack == false) return;
    
            difference = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);

            if (angle < 90 && angle > -90)
            {
                swordScale.y = -1f;
                swordScale.x = +1f;
            }
            else
            {
                swordScale.y = +1f;
                swordScale.x = +1f;
            }    

            transform.localScale = swordScale;
        }

        public void ResetTheRotation()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            swordScale.y = 1f;
            transform.localScale = swordScale;
        }
    }
}
