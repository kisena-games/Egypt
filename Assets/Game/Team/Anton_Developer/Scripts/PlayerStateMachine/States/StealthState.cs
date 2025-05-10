using UnityEngine;

public class StealthState : State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;

    private readonly float _moveSpeed;
    private readonly float _speedChangeRate;
    private readonly float _rotationSmoothTime;

    private float _speed;
    private float _animationBlend;
    private float _verticalVelocity;
    private bool _isGrounded;

    private readonly int _animIDSpeed = Animator.StringToHash("Speed");
    private readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

    public override void OnEnter()
    {
        _animator.SetBool("Stealth", true);
    }
    public StealthState(Animator animator, CharacterController controller, Camera camera, Transform transform,
        float moveSpeed, float speedChangeRate, float rotationSmoothTime)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;

        _moveSpeed = moveSpeed;
        _speedChangeRate = speedChangeRate;
        _rotationSmoothTime = rotationSmoothTime;
    }

    public override void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = InputManager.Instance.MoveInputNormalized;
        float targetSpeed = _moveSpeed;

        if (input == Vector2.zero)
            targetSpeed = 0.0f;

        _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime * _speedChangeRate);
        _speed = Mathf.Round(_speed * 1000f) / 1000f;

        _animationBlend = Mathf.Lerp(_animationBlend, _speed, Time.deltaTime * _speedChangeRate);
        if (_animationBlend < 0.01f)
            _animationBlend = 0f;

        Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camRight = _camera.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camRight * input.x + camForward * input.y;
            moveDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation, _rotationSmoothTime);

            Vector3 movement = moveDirection * (_speed * Time.deltaTime);
            movement += Vector3.up * _verticalVelocity * Time.deltaTime;

            _controller.Move(movement);
        }
        else
        {
            _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }


        if (_animator)
        {
            if (_isGrounded)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, 1);
            }
        }
    }
}

