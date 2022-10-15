using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;
using Features.Player.Scripts;
using UnityEngine;

namespace Features.Sword.Scripts
{
    public class SwordAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private SwordMoving swordMoving;

        [SerializeField] private AudioAsset _swingAudio;
        [SerializeField] private AudioAsset _damageAudio;

        private AudioHandler _audioHandler;
        
        private Animator animator;
        private bool isAttack = false;
        Vector2 direction;

        private void Start()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isAttack == false)
            {
                isAttack = true;
                animator.SetTrigger("Attack");
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

                _audioHandler = Audio.Play(_swingAudio);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isAttack == false)
                return;
            
            _audioHandler.Stop();
            _audioHandler = Audio.Play(_damageAudio);

            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }

        public void EndOfAttack()
        {
            isAttack = false;
            GetComponent<BoxCollider2D>().enabled = false;
            swordMoving.isAttack = false;
            swordMoving.ResetTheRotation();
        }
        public void StartAttack()
        {
            swordMoving.isAttack = true;
        }

        public void EnableCollider()
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
