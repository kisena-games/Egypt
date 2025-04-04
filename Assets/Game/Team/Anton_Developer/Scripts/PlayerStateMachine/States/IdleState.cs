using UnityEngine;

public class IdleState : State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;

    public IdleState(Animator animator, CharacterController controller)
    {
        _animator = animator;
        _controller = controller;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("Walk", false);
        _animator.SetBool("Run", false);
    }

    public override void OnUpdate()
    {
        _controller.Move(Vector3.zero);
    }
}
