using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.View;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IUpdatableCoroutineRunner iUpdatableCoroutineRunner;
        public bool IsActive { get; private set; }

        public BootstrapState(StateMachine stateMachine,
            IUpdatableCoroutineRunner iUpdatableCoroutineRunner,
            InitialData initialData,
            ServiceContainer serviceContainer)
        {
            _stateMachine = stateMachine;
            this.iUpdatableCoroutineRunner = iUpdatableCoroutineRunner;
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
            serviceContainer.Register<IUpdatableCoroutineRunner>(iUpdatableCoroutineRunner);
            
        }
    }
}