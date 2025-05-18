using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public StateMachine(State defaultState)
    {
        SetState(defaultState);
    }

    public void OnUpdate()
    {
        int newIndex = IsSuccessConditionsOfTransitionToNextState();

        if (newIndex != -1)
        {
            SetState(CurrentState.Transitions[newIndex].NextState);
        }
        else
        {
            CurrentState.OnUpdate();
        }
    }

    public void OnFixedUpdate()
    {
        CurrentState.OnFixedUpdate();
    }

    private int IsSuccessConditionsOfTransitionToNextState()
    {
        List<ITransition> currentTransitions = CurrentState.Transitions;
        for (var i = 0; i < currentTransitions.Count; i++)
        {
            ICondition condition = currentTransitions[i].Condition;
            condition.OnUpdate();
            if (condition.IsConditionSuccess())
            {
                return i;
            }
        }

        return -1;
    }

    public void SetState(State state)
    {
        CurrentState?.OnExit();
        CurrentState?.DeInitializeTransitions();

        CurrentState = state;
        CurrentState.OnEnter();
        CurrentState.InitializeTransitions();
    }
}
