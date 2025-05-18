using Game.Scripts.Services;
using Game.Scripts.Services.View;
using Game.Scripts.UI;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }
        
        private MenuView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<MenuView>();
        private MenuView _view;
        
        public MenuState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            
        }

        public void Enter()
        {
            IsActive = true;
        }

        public void Exit()
        {
            IsActive = false;
            View.Hide();
        }
        
        private void ShowSettings()
        {
            ServiceContainer.Container.Get<IViewContainer>().GetView<PauseView>().Show();
        }

        private void PlayClick()
        {
            _stateMachine.Enter<GameplayState>();
        }
    }
}