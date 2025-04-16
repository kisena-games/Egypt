using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerJumpState : State
{
    private const string JUMP_ANIM_KEY = "Jump";

    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _playerTransform;
    private readonly float _jumpForce;
    private readonly float _rotationSpeed;
    private readonly float _gravity;

    private Vector3 _velocity;

    public PlayerJumpState(Animator animator, CharacterController controller, Camera camera, Transform playerTransform, float jumpForce, float rotationSpeed, float gravity)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _playerTransform = playerTransform;
        _jumpForce = jumpForce;
        _rotationSpeed = rotationSpeed;
        _gravity = gravity;
    }

    public void Enter()
    {
        _animator.SetBool(JUMP_ANIM_KEY, true);
        _velocity.y = _jumpForce;
        Debug.Log("jump");
    }

    public override void OnUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontal, 0f, vertical);

        Vector3 move = _camera.transform.TransformDirection(input);
        move.y = 0f;
        move.Normalize();

        if (move.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            _playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        _velocity.y -= _gravity * Time.deltaTime;

        Vector3 movement = move * Time.deltaTime;
        movement += Vector3.up * _velocity.y * Time.deltaTime;

        _controller.Move(movement);
    }

    public void Exit()
    {
        _animator.SetBool(JUMP_ANIM_KEY, false);
        Debug.Log("ExitJump");
    }
}
