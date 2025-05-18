using System;
using Game.Scripts.Services;
using Game.Scripts.Services.View;
using Game.Scripts.UI;
using JetBrains.Annotations;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class LoseState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }
        
        private LoseView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<LoseView>();
        private LoseView _view;
        
        public LoseState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void Enter()
        {
            IsActive = true;
            View.Show();
        }

        private void NextClick()
        {
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
            IsActive = false;
            View.Hide();
        }
    }
}