using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncStateCondition : StateCondition
{
    private Func<bool> returnValue;

    public FuncStateCondition(Func<bool> returnValue)
    {
        this.returnValue = returnValue;
    }

    public override bool IsConditionSuccess()
    {
        return returnValue.Invoke();
    }
}
