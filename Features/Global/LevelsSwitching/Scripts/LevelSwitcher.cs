using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Global.LevelsSwitching.Scripts
{
    [DisallowMultipleComponent]
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private ScenesConfig _config;
        
        [SerializeField] private GameObject _loadingScreen;

        public ScenesConfig Config => _config;
        
        public void LoadScene(string scene)
        {
            StopAllCoroutines();

            StartCoroutine(LoadAsync(scene));
        }

        private IEnumerator LoadAsync(string scene)
        {
            _loadingScreen.SetActive(true);

            var operation = SceneManager.LoadSceneAsync(scene);

            while (operation.isDone == false)
                yield return null;
            
            yield return new WaitForSeconds(1f);
            
            _loadingScreen.SetActive(false);
        }
    }
}