using System;
using App.Scripts.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Services.SceneLoader
{
    public class SceneLoaderService : ISceneLoader 
    {
        public event Action<float> OnUpdateProgressBar;
        public event Action OnShowLoadScreen;
        
        private readonly IUpdatableCoroutineRunner _coroutineRunner;

        public SceneLoaderService(IUpdatableCoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }


        public void LoadSceneAsync(string sceneName, System.Action onLoaded)
        {
            _coroutineRunner.StartCoroutine(LoadCoroutine(sceneName, onLoaded));
        }

        private System.Collections.IEnumerator LoadCoroutine(string sceneName, System.Action onLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                OnUpdateProgressBar?.Invoke(asyncLoad.progress);
            
                if (asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
                {
                    Debug.Log(asyncLoad.progress);

                    yield return new WaitForSeconds(1f);
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
            Debug.Log("Acync");
            onLoaded?.Invoke();
        }
    }
}