using System;
using App.Scripts.Services;
using App.Scripts.Services.SceneLoader;
using App.Scripts.Services.View;
using App.Scripts.UI;
using UnityEngine;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class GameLoadState : IPayloadState<GameLoadPayload>
    {
        private readonly StateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader; 

        public bool IsActive { get; private set; }

        private LoadView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<LoadView>();
        private LoadView _view;

        private GameLoadPayload _currentPayload;

        public GameLoadState(
            StateMachine stateMachine,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(GameLoadPayload payload)
        {
            AddListener();
            View.Show();
            _currentPayload = payload;
            _sceneLoader.LoadSceneAsync(payload.SceneName, OnSceneLoaded);
            IsActive = true;
        }

        public void Exit()
        {
            RemoveListener();
            View.Hide();
            IsActive = false;
        }

        private void AddListener()
        {
            _sceneLoader.OnShowLoadScreen += View.ShowLoadingScreen;
            _sceneLoader.OnUpdateProgressBar += View.UpdateProgressBar;
        }

        private void RemoveListener()
        {
            _sceneLoader.OnShowLoadScreen -= View.ShowLoadingScreen;
            _sceneLoader.OnUpdateProgressBar -= View.UpdateProgressBar;
        }
        
        private void OnSceneLoaded()
        {
            _currentPayload?.OnLoadedCallback?.Invoke();
        }
    }
}