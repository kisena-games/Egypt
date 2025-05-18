using System;
using System.Collections.Generic;
using App.Scripts.Bootstrap.StateMachine.States;
using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.View;
using App.Scripts.UI;

namespace App.Scripts.Bootstrap.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public StateMachine(IUpdatableCoroutineRunner iUpdatableCoroutineRunner,
            InitialData initialData,
            ServiceContainer serviceContainer)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(BootstrapState), new BootstrapState(
                    stateMachine: this, 
                    iUpdatableCoroutineRunner: iUpdatableCoroutineRunner,
                    initialData: initialData,
                    serviceContainer: serviceContainer
                )},
                {typeof(GameLoadState), new GameLoadState(
                    stateMachine: this
                )},
                {typeof(MenuState), new MenuState(
                    stateMachine: this
                )},
                {typeof(GameplayState), new GameplayState(
                    stateMachine: this,
                    initialData: initialData.gameplayData
                )},
                {typeof(WinState), new WinState(
                    stateMachine: this
                )},
                {typeof(LoseState), new LoseState(
                    stateMachine: this
                )},
            };
            
            serviceContainer.Get<IViewContainer>().GetView<DebugView>().Initialize(this, GetState<GameplayState>());
        }
        
        public void Enter<T>() where T : class, IState
        {
            var state = ChangeState<T>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}