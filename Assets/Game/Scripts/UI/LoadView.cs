using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class LoadView : BaseView
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] Slider _loadingBar;

        [SerializeField] private string _sceneName;

        [ContextMenu("Load Scene (Test)")]
        public void Loading()
        {
            Loading(_sceneName);
        }

        public void Loading(string sceneName)
        {
            _loadingScreen.SetActive(true);
        
            StartCoroutine(LoadAsync(sceneName));
        }

        private IEnumerator LoadAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                _loadingBar.value = asyncLoad.progress;
            
                if (asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
                {
                    yield return new WaitForSeconds(1f);
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    
    }
}
