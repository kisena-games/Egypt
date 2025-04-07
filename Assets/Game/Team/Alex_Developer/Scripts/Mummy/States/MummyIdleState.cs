using UnityEngine;

public class MummyIdleState:State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;

    public MummyIdleState(Animator animator, CharacterController controller)
    {
        _animator = animator;
        _controller = controller;
    }

    public override void OnEnter()
    {
        _animator?.SetBool("Idle", true);
        _animator?.SetBool("Patrolling", false);
        Debug.Log("Idle");
    }

    public override void OnUpdate()
    {
        _controller.Move(Vector3.zero);
    }
}
