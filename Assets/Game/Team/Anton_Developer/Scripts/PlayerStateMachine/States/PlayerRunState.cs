using UnityEngine;

public class PlayerRunState : State
{
    private const string RUN_ANIM_KEY = "Run";

    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;
    private readonly float _runSpeed;
    private readonly float _rotationSpeed;
    private readonly float _gravity;

    public PlayerRunState(Animator animator, CharacterController controller, Camera camera, Transform transform, float runSpeed, float rotationSpeed, float gravity)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;
        _runSpeed = runSpeed;
        _rotationSpeed = rotationSpeed;
        _gravity = gravity;
    }

    public override void OnEnter()
    {
        _animator.SetBool(RUN_ANIM_KEY, true);
    }

    public override void OnExit()
    {
        _animator.SetBool(RUN_ANIM_KEY, false);
    }

    public override void OnUpdate()
    {
        Vector2 input = InputManager.Instance.MoveInputNormalized;
        Vector3 move = new Vector3(input.x, 0, input.y);
        Vector3 motion = Vector3.zero;

        if (move.magnitude >= 0.1f)
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camRight = _camera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * input.y + camRight * input.x;
            moveDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);

            motion = moveDirection * _runSpeed;
        }

        // Применяем гравитацию
        motion += Vector3.down * _gravity * Time.deltaTime;

        _controller.Move(motion * Time.deltaTime);
    }
}
