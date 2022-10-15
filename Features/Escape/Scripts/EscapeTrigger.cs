using System;
using System.Collections;
using Features.Global.LevelsSwitching.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Escape.Scripts
{
    [DisallowMultipleComponent]
    public class EscapeTrigger : MonoBehaviour
    {
        [SerializeField] private float _time;
        
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Destroy(col.gameObject);

            StartCoroutine(Process());
        }

        private IEnumerator Process()
        {
            var color = _image.color;

            while (color.a < 1f)
            {
                color.a += _time * Time.deltaTime;
                _image.color = color;
                yield return null;
            }
            
            yield return new WaitForSeconds(1f);
            
            color = _text.color;

            while (color.a < 1f)
            {
                color.a += _time * Time.deltaTime;
                _text.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(10f);
            
            Transitions.LoadMenu();
        }
    }
}