using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class DashProgressionUI : MonoBehaviour
    {
        [SerializeField] private float _fillSpeed = 1f;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            _image.fillAmount = 0f;
        }
        
        private void Update()
        {
            _image.fillAmount = Mathf.Lerp(_image.fillAmount, PlayerMovement.DashProgress, _fillSpeed * Time.deltaTime);
        }
    }
}