using System;
using TMPro;
using UnityEngine;

namespace Features.Tutorial
{
    [DisallowMultipleComponent]
    public class DialogueWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private TMP_Text _text;

        private Action _callback;
        
        public void Open(string text, Action callback)
        {
            _window.SetActive(true);
            _text.text = text;
            _callback = callback;
        }

        public void OnNextClicked()
        {
            _callback?.Invoke();
            _window.SetActive(false);
        }
    }
}