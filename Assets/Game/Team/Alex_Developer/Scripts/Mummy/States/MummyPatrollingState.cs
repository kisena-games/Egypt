using UnityEngine;

public class MummyPatrollingState:State
{
    private readonly Animator _animator;

    public MummyPatrollingState(Animator animator)
    {
        _animator = animator;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Patrolling", true);
        Debug.Log("Patrolling");
    }

    public override void OnExit()
    {
        _animator.SetBool("Patrolling", false);
    }

    public override void OnUpdate()
    {
        
    }
}

