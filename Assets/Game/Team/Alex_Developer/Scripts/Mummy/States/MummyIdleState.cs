using UnityEngine;

public class MummyIdleState : State
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
        _animator.SetBool("Idle", true);
        Debug.Log("Idle");
    }

    public override void OnExit() 
    {
        _animator.SetBool("Idle", false);
    }

    public override void OnUpdate()
    {
        _controller.Move(Vector3.zero);
    }
}
