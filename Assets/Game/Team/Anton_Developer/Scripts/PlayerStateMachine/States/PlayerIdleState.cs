using UnityEngine;

public class PlayerIdleState : State
{
    private const string IDLE_ANIM_KEY = "Idle";

    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly float _gravity;

    public PlayerIdleState(Animator animator, CharacterController controller, float gravity)
    {
        _animator = animator;
        _controller = controller;
        _gravity = gravity;
    }

    public override void OnEnter()
    {
        _animator.SetBool(IDLE_ANIM_KEY, true);
    }

    public override void OnExit()
    {
        _animator.SetBool(IDLE_ANIM_KEY, false);
    }

    public override void OnUpdate()
    {
        if (!_controller.isGrounded)
        {
            _controller.Move(Vector3.down * _gravity * Time.deltaTime);
        }
    }
}
