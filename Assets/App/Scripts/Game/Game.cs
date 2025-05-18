using App.Scripts.Bootstrap;
using App.Scripts.Bootstrap.StateMachine.States;
using App.Scripts.Data;
using App.Scripts.Services;

namespace App.Scripts.Game
{
    public class Game
    {
        private readonly App.Scripts.Bootstrap.StateMachine.StateMachine _stateMachine;
        public Game(IUpdatableCoroutineRunner iUpdatableCoroutineRunner, InitialData initialData)
        {
            _stateMachine = new App.Scripts.Bootstrap.StateMachine.StateMachine(
                iUpdatableCoroutineRunner,
                initialData,
                ServiceContainer.Container);
            
            _stateMachine.Enter<BootstrapState>();
        }
    }
}