using UnityEngine;

public class PlayerIdleState : State
{
    private const string IDLE_ANIM_KEY = "Idle";
    private const string JUMP_ANIM_KEY = "Jump";

    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly float _gravity;
    private readonly float _jumpForce;

    private float _verticalVelocity;
    private bool _isJumping;

    public PlayerIdleState(Animator animator, CharacterController controller, float gravity, float jumpForce)
    {
        _animator = animator;
        _controller = controller;
        _gravity = gravity;
        _jumpForce = jumpForce;
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
        HandleJump();
    }

    private void HandleJump()
    {
        if (_controller.isGrounded)
        {
            if (_isJumping)
            {
                // Приземлились
                _isJumping = false;
                _animator.SetBool("Jump", false);
            }

            _verticalVelocity = -0.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
                _isJumping = true;
                _animator.SetBool("Jump", true);
            }
        }
        else
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(0f, _verticalVelocity, 0f);
        _controller.Move(move * Time.deltaTime);
    }
}
