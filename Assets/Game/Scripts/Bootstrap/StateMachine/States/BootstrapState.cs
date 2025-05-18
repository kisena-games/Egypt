using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Core;
using Game.Scripts.Services;
using Game.Scripts.Services.View;

namespace Game.Scripts.Bootstrap.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        public bool IsActive { get; private set; }

        public BootstrapState(StateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            InitialData initialData,
            ServiceContainer serviceContainer)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            RegisterServices(serviceContainer, initialData);
        }

        public void Enter()
        {
            IsActive = true;
            EnterLoadingState();
        }

        private void EnterLoadingState()
        {
            _stateMachine.Enter<GameLoadState>();
        }

        public void Exit()
        {
            IsActive = false;
        }

        private void RegisterServices(ServiceContainer serviceContainer, InitialData initialData)
        {
            var viewContainer = serviceContainer.Register<IViewContainer>(new ViewContainer(initialData.UIInitialData));

        }
    }
}