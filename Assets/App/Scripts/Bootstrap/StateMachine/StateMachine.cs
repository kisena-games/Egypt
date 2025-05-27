using System;
using System.Collections.Generic;
using App.Scripts.Bootstrap.StateMachine.States;
using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.SceneLoader;
using App.Scripts.Services.View;
using App.Scripts.UI;

namespace App.Scripts.Bootstrap.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _activeState;

        public StateMachine(IUpdatableCoroutineRunner iUpdatableCoroutineRunner,
            InitialData initialData,
            ServiceContainer serviceContainer)
        {
            _states = new Dictionary<Type, IExitState>
            {
                {
                    typeof(BootstrapState), new BootstrapState(
                        stateMachine: this,
                        updatableCoroutineRunner: iUpdatableCoroutineRunner,
                        initialData: initialData,
                        serviceContainer: serviceContainer
                    )
                },
                {
                    typeof(GameLoadState), new GameLoadState(
                        stateMachine: this,
                        serviceContainer.Get<ISceneLoader>()
                    )
                },
                {
                    typeof(MenuState), new MenuState(
                        stateMachine: this
                    )
                },
                {
                    typeof(GameplayState), new GameplayState(
                        stateMachine: this
                    )
                },
                {
                    typeof(WinState), new WinState(
                        stateMachine: this
                    )
                },
                {
                    typeof(LoseState), new LoseState(
                        stateMachine: this
                    )
                },
            };

            serviceContainer.Get<IViewContainer>().GetView<DebugView>()
                .Initialize(this, GetState<GameplayState>());
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}