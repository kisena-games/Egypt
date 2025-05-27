using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.SceneLoader;
using App.Scripts.Services.View;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IUpdatableCoroutineRunner _updatableCoroutineRunner;
        public bool IsActive { get; private set; }

        public BootstrapState(StateMachine stateMachine,
            IUpdatableCoroutineRunner updatableCoroutineRunner,
            InitialData initialData,
            ServiceContainer serviceContainer)
        {
            _stateMachine = stateMachine;
            this._updatableCoroutineRunner = updatableCoroutineRunner;
            RegisterServices(serviceContainer, initialData);
        }

        public void Enter()
        {
            IsActive = true;
            EnterLoadingState();
        }

        private void EnterLoadingState()
        {
            _stateMachine.Enter<GameLoadState, GameLoadPayload>(new GameLoadPayload(SceneNameConstants.Menu,
                () => _stateMachine.Enter<MenuState>()));
        }

        public void Exit()
        {
            IsActive = false;
        }

        private void RegisterServices(ServiceContainer serviceContainer, InitialData initialData)
        {
            serviceContainer.Register<IViewContainer>(new ViewContainer(initialData.UIInitialData));
            serviceContainer.Register<IUpdatableCoroutineRunner>(_updatableCoroutineRunner);
            serviceContainer.Register<ISceneLoader>(new SceneLoaderService(_updatableCoroutineRunner));
        }
    }
}