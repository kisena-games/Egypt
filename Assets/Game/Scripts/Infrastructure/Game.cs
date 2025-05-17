using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Game.Scripts.Infrastructure.Core;
using Game.Scripts.Infrastructure.Data;
using Game.Scripts.Infrastructure.StateMachine;
using Game.Scripts.Infrastructure.StateMachine.States;

namespace Game.Scripts.Infrastructure
{
    public class Game
    {
        private readonly StateMachine.StateMachine _stateMachine;
        public Game(ICoroutineRunner coroutineRunner, InitialData initialData)
        {
            /*_stateMachine = new StateMachine.StateMachine(
                coroutineRunner,
                initialData,
                ServiceContainer.Container);
            
            _stateMachine.Enter<BootstrapState>();*/
        }
    }
}