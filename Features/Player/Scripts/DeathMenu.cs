using Features.Global.LevelsSwitching.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _body;

        private void Start()
        {
            _body.SetActive(false);
        }
        
        private void OnEnable()
        {
            Health.Died += OnDeath;
        }
        
        private void OnDisable()
        {
            Health.Died -= OnDeath;
        }

        private void OnDeath()
        {
            _body.SetActive(true);
        }
        
        public void OnMenuClicked()
        {
            Transitions.LoadMenu();
        }

        public void RestartLoop()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}