using App.Scripts.Services;
using App.Scripts.Services.View;
using App.Scripts.UI;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class GameLoadState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }
        
        private LoadView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<LoadView>();
        private LoadView _view;

        public GameLoadState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            IsActive = true;
            View.Show();
            LoadingComplete();
        }

        public void Exit()
        {
            View.Hide();
            IsActive = false;
        }

        private void LoadingComplete()
        {
            _stateMachine.Enter<GameplayState>();
        }
        
    }
}