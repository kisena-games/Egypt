using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCondition : ICondition
{
    public abstract bool IsConditionSuccess();

    public virtual void OnUpdate() { }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }
}
