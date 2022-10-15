using UnityEngine;

namespace Features.Tutorial
{
    [DisallowMultipleComponent]
    public class TutorialTrigger : MonoBehaviour
    {
        private bool _isTriggered;

        public bool IsTriggered => _isTriggered;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            _isTriggered = true;
        }
    }
}