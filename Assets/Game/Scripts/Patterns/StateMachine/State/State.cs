using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public List<ITransition> Transitions { get; private set; } = new List<ITransition>();

    public State() { }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }

    public void InitializeTransitions()
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.OnEnter();
        }
    }

    public void DeInitializeTransitions()
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.OnExit();
        }
    }

    public void AddTransition(StateTransition transition)
    {
        Transitions.Add(transition);
    }

    public void RemoveTransition(StateTransition transition)
    {
        if (Transitions.Contains(transition))
        {
            Transitions.Remove(transition);
        }
    }
}
