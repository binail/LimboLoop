using System.Collections;
using Features.Enemy.Scripts.Waves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _front;
        [SerializeField] private TMP_Text _behind;

        [SerializeField] private float _delay = 6f;
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _time = 4f;

        private void Start()
        {
            _front.text = $"LOOP {WavesProcessor._waveNumber + 1}/9";
            _behind.text = $"LOOP {WavesProcessor._waveNumber + 1}/9";
            
            SetAlpha(_front, 0f);
            SetAlpha(_behind, 0f);

            StartCoroutine(Display());
        }

        private IEnumerator Display()
        {
            yield return new WaitForSeconds(_delay);
            
            while (_front.color.a < 1f)
            {
                var alpha = _front.color.a + Time.deltaTime * _speed;
                
                SetAlpha(_front, alpha);
                SetAlpha(_behind, alpha);
                
                yield return null;
            }
            
            yield return new WaitForSeconds(_time);
            
            while (_front.color.a > 0f)
            {
                var alpha = _front.color.a - Time.deltaTime * _speed;
                
                SetAlpha(_front, alpha);
                SetAlpha(_behind, alpha);
                
                yield return null;
            }
        }

        private void SetAlpha(Graphic text, float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}