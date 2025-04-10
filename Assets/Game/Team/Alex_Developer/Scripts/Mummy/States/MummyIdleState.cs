using UnityEngine;

public class MummyIdleState : State
{
    private readonly Animator _animator;

    public MummyIdleState(Animator animator)
    {
        _animator = animator;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Idle", true);
        Debug.Log("Idle");
    }

    public override void OnExit() 
    {
        _animator.SetBool("Idle", false);
    }

    public override void OnUpdate()
    {
        
    }
}
