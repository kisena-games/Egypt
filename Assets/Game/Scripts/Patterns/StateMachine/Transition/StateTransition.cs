using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition : ITransition
{
    public State NextState { get; private set; }
    public ICondition Condition { get; private set; }

    public StateTransition(State nextState, StateCondition condition)
    {
        NextState = nextState;
        Condition = condition;
    }

    public void OnEnter() 
    {
        Condition.OnEnter();
    }

    public void OnExit()
    {
        Condition.OnExit();
    }

    public void OnUpdate()
    {
        Condition.OnUpdate();
    }
}
