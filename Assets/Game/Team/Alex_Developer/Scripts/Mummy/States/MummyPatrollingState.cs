using UnityEngine;

public class MummyPatrollingState:State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;

    public MummyPatrollingState(Animator animator, CharacterController controller)
    {
        _animator = animator;
        _controller = controller;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Patrolling", true);
        //Debug.Log("Patrolling");
    }

    public override void OnExit()
    {
        _animator.SetBool("Patrolling", false);
    }

    public override void OnUpdate()
    {
        _controller.Move(Vector3.zero);
    }
}

