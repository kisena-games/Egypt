using Game.Scripts.Services;
using Game.Scripts.Services.View;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class WinState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }
        
        public WinState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            IsActive = true;
        }

        private void NextClick()
        {
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
            IsActive = false;
        }

    }
}