using Game.Scripts.Data;
using Game.Scripts.Services;
using Game.Scripts.Services.View;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly InitialGameplayData _initialData;
        public bool IsActive { get; private set; }

        private GameplayView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<GameplayView>();
        private GameplayView _view;
        

        public GameplayState(
            StateMachine stateMachine,
            InitialGameplayData initialData)
        {
            _stateMachine = stateMachine;
            _initialData = initialData;
        }

        public void Enter()
        {
            IsActive = true;
            View.Show();
            LoadLevel();
        }

        private void ShowPause()
        {
            ServiceContainer.Container.Get<IViewContainer>().GetView<PauseView>().Show();
        }

        private void RestartClick()
        {
            _stateMachine.Enter<GameplayState>();
        }

        private void HomeClick()
        {
            _stateMachine.Enter<MenuState>();
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