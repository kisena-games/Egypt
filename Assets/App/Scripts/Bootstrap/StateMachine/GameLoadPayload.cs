using System;
using App.Scripts.Services;
using App.Scripts.Services.SceneLoader;

namespace App.Scripts.Bootstrap.StateMachine
{
    public class GameLoadPayload : IService
    {
        public string SceneName { get; }
        public Action OnLoadedCallback { get; }

        public GameLoadPayload(string sceneName, Action onLoadedCallback)
        {
            SceneName = sceneName;
            OnLoadedCallback = onLoadedCallback;
        }
    }
}