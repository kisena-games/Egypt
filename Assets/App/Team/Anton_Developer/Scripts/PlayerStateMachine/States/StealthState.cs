using UnityEngine;

public class StealthState : State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;
    private readonly LayerMask _groundLayers;

    private readonly float _moveSpeed;
    private readonly float _speedChangeRate;
    private readonly float _rotationSmoothTime;

    private float _speed;
    private float _animationBlend;
    private float _verticalVelocity;
    private bool _isGrounded;
    private float _groundedRadius = 0.28f;
    private float _groundedOffset = -0.14f;
    private float _terminalVelocity = 53.0f;
    private float _gravity = -9.81f;
    private float _fallTimeoutDelta;
    private float _fallTimeout = 0.15f;

    private readonly int _animIDSpeed = Animator.StringToHash("StealthSpeed");
    private readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    private readonly int _animIDGrounded = Animator.StringToHash("Grounded");

    public override void OnEnter()
    {
        _animator.SetBool("Stealth", true);
        _controller.height = 0.9f;
        _controller.center = new Vector3(0, 0.5f, 0);
    }
    public StealthState(Animator animator, CharacterController controller, Camera camera, Transform transform,
        float moveSpeed, float speedChangeRate, float rotationSmoothTime, LayerMask groundLayers)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;
        _groundLayers = groundLayers;

        _moveSpeed = moveSpeed;
        _speedChangeRate = speedChangeRate;
        _rotationSmoothTime = rotationSmoothTime;
    }

    public override void OnUpdate()
    {
        Move();
        GroundedCheck();
        ApplyGravity();
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

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(_transform.position.x, _transform.position.y - _groundedOffset,
                _transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers,
                QueryTriggerInteraction.Ignore);

        if (_animator)
        {
            _animator.SetBool(_animIDGrounded, _isGrounded);
        }

    }

    private void ApplyGravity()
    {
        if (_isGrounded)
        {
            _fallTimeoutDelta = _fallTimeout;

            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;
        }
        else
        {
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            _verticalVelocity += _gravity * Time.deltaTime;

            if (_verticalVelocity < -_terminalVelocity)
                _verticalVelocity = -_terminalVelocity;
        }
    }
}

