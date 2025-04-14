using UnityEngine;

public class PlayerJumpState : State
{
    private const string JUMP_ANIM_KEY = "Jump";

    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;
    private readonly float _rotationSpeed;
    private readonly float _jumpForce;
    private readonly float _gravity;
    private float _verticalVelocity;
    private Vector3 _horizontalVelocity;

    public PlayerJumpState(Animator animator, CharacterController controller, Camera camera, Transform transform, float jumpForce, float gravity, float rotationSpeed, Vector3 initialVelocity)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;
        _jumpForce = jumpForce;
        _gravity = gravity;
        _rotationSpeed = rotationSpeed;
        _verticalVelocity = jumpForce;
        _horizontalVelocity = initialVelocity;
    }

    public override void OnEnter()
    {
        _verticalVelocity = _jumpForce; // Устанавливаем заново при входе
        _animator.SetBool(JUMP_ANIM_KEY, true);
        Debug.Log($"Jump started: JumpForce={_jumpForce}, VerticalVelocity={_verticalVelocity}");
    }

    public override void OnExit()
    {
        _animator.SetBool(JUMP_ANIM_KEY, false);
        Debug.Log("Jump ended");
    }

    public override void OnUpdate()
    {
        // Применяем гравитацию
        _verticalVelocity -= _gravity * Time.deltaTime;

        // Обработка горизонтального движения
        Vector2 input = InputManager.Instance.MoveInputNormalized;
        Vector3 move = new Vector3(input.x, 0, input.y);

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

            // Обновляем горизонтальную инерцию
            _horizontalVelocity = moveDirection * _horizontalVelocity.magnitude;
        }

        // Формируем вектор движения
        Vector3 motion = _horizontalVelocity + Vector3.up * _verticalVelocity;
        _controller.Move(motion * Time.deltaTime);

        Debug.Log($"Jump Update: VerticalVelocity={_verticalVelocity}, Grounded={_controller.isGrounded}");

        // Проверка приземления
        if (_controller.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -_gravity * Time.deltaTime; // Мягкий сброс для стабильности
        }
    }
}
