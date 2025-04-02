using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishCondition : StateCondition
{
    private readonly Animator _animator;
    private readonly string _name;
    private readonly int _layerIndex;
    private readonly float _finishTime;

    public AnimationFinishCondition(Animator animator, string name, int layerIndex, float finishTime = 0.8f)
    {
        _animator = animator;
        _name = name;
        _layerIndex = layerIndex;
        _finishTime = finishTime;
    }

    public override bool IsConditionSuccess()
    {
        return _animator.GetCurrentAnimatorStateInfo(_layerIndex).normalizedTime > _finishTime && _animator
            .GetCurrentAnimatorStateInfo(_layerIndex)
            .IsName(_name);
    }
}
