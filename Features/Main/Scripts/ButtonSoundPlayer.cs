using Features.Global.AudioPlaying.Scripts;
using Features.Global.AudioPlaying.Scripts.Entity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Main.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ButtonSoundPlayer : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private AudioAsset _clickAudio;
        [SerializeField] private AudioAsset _selectAudio;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Audio.Play(_clickAudio);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Audio.Play(_selectAudio);
        }
    }
}