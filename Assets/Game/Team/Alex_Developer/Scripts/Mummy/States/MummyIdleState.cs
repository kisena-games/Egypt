using UnityEngine;
using UnityEngine.AI;

public class MummyIdleState : State
{
    private readonly Animator _animator;
    private static NavMeshAgent _agent;
    public MummyIdleState(Animator animator, NavMeshAgent agent)
    {
        _animator = animator;
        _agent = agent;
    }

    public override void OnEnter()
    {
        Debug.Log("Idle");
    }

    public override void OnExit() 
    {

    }

    public override void OnUpdate()
    {
        if (_agent.velocity.magnitude < 1) _animator.SetBool("Patrolling", false);
        else _animator.SetBool("Patrolling", true);
    }
}
