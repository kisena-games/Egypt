using System;

namespace App.Scripts.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        public event Action<float> OnUpdateProgressBar;
        public event Action OnShowLoadScreen;
        
        void LoadSceneAsync(string sceneName, System.Action onLoaded);
    }
}