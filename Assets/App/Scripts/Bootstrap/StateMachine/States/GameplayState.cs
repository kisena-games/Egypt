using App.Scripts.Core;
using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.View;
using App.Scripts.UI;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }

        private GameplayView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<GameplayView>();
        private GameplayView _view;
        
        public GameplayState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine;

            ServiceContainer.Container.Get<IViewContainer>().GetView<PauseView>().DebugPanelOpen += () => 
                ServiceContainer.Container.Get<IViewContainer>().ShowView<DebugView>();
            ServiceContainer.Container.Get<IUpdatableCoroutineRunner>().OnUpdate += Update;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        
        public void Enter()
        {
            EntryPoint entryPoint = GameObject.FindFirstObjectByType<EntryPoint>();
            entryPoint.PlayerLoadScene.OnLoadScene += LoadScene;
            var inventoryView = ServiceContainer.Container.Get<IViewContainer>().GetView<InventoryView>();
            inventoryView.Show();
            inventoryView.Initialize(entryPoint.PlayerInventory);
            View.Show();
            LoadLevel();
            IsActive = true;
        }

        private void LoadScene(string sceneName)
        {
            _stateMachine.Enter<GameLoadState, GameLoadPayload>(new GameLoadPayload(sceneName,
                () => _stateMachine.Enter<GameplayState>()));
        }
        
        private void Pause()
        {
            ServiceContainer.Container.Get<IViewContainer>().GetView<PauseView>().Pause();
        }
        
        private void Resume()
        {
            ServiceContainer.Container.Get<IViewContainer>().GetView<PauseView>().Resume();
        }

        private void LevelLose()
        {
            _stateMachine.Enter<LoseState>();
        }

        private void LevelComplete()
        {
            _stateMachine.Enter<WinState>();
        }

        public void Exit()
        {
            IsActive = false;
            View.Hide();
        }

        private void LoadLevel()
        {
        }
    }
}