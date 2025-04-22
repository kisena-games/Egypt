using UnityEngine;
using UnityEngine.AI;

public class MummyIdleState : State
{
    private const string IDLE_ANIM_KEY = "Idle";

    private readonly Animator _animator;
    public MummyIdleState(Animator animator)
    {
        _animator = animator;
    }

    public override void OnEnter()
    {
        _animator.SetBool(IDLE_ANIM_KEY, true);
    }

    public override void OnExit() 
    {
        _animator.SetBool(IDLE_ANIM_KEY, false);
    }
}
