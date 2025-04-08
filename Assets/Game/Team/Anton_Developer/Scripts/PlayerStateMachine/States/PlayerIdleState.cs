using UnityEngine;

public class PlayerIdleState : State
{
    private const string IDLE_ANIM_KEY = "Idle";

    private readonly Animator _animator;

    public PlayerIdleState(Animator animator)
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
