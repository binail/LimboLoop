using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class KatanaProgressionUI : MonoBehaviour
    {
        [SerializeField] private float _fillSpeed = 1f;

        private float _targetProgress;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            _image.fillAmount = 0f;
        }
        
        private void OnEnable()
        {
            KillCounter.EnemyKilled += OnEnemyKilled;
        }
        
        private void OnDisable()
        {
            KillCounter.EnemyKilled -= OnEnemyKilled;
        }

        private void Update()
        {
            _image.fillAmount = Mathf.Lerp(_image.fillAmount, _targetProgress, _fillSpeed * Time.deltaTime);
        }

        private void OnEnemyKilled(float progress)
        {
            _targetProgress = progress;
        }
    }
}