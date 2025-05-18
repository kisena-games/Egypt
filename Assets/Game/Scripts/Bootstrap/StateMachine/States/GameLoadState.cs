using Game.Scripts.Services;
using Game.Scripts.Services.View;
using Game.Scripts.UI;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class GameLoadState : IState
    {
        private readonly Game.Scripts.Bootstrap.StateMachine.StateMachine _stateMachine;
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
        }

        public void Exit()
        {
            View.Hide();
            IsActive = false;
        }

        private void LoadingComplete()
        {
            _stateMachine.Enter<MenuState>();
        }
        
    }
}