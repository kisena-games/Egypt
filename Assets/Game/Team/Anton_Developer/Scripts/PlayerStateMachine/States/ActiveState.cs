using UnityEngine;
public class ActiveState : State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;
    private readonly LayerMask _groundLayers;

    private readonly float _gravity;
    private readonly float _moveSpeed;
    private readonly float _sprintSpeed;
    private readonly float _speedChangeRate;
    private readonly float _jumpForce;
    private readonly float _rotationSmoothTime;
    private readonly float _fallTimeout;

    private float _speed;
    private float _animationBlend;
    private bool _isGrounded = true;
    private float _fallTimeoutDelta;
    private float _verticalVelocity;
    private float _jumpTimeoutDelta;
    private float _terminalVelocity = 53.0f;
    private float _groundedRadius = 0.28f;
    private float _groundedOffset = -0.14f;

    private readonly int _animIDSpeed = Animator.StringToHash("Speed");
    private readonly int _animIDGrounded = Animator.StringToHash("Grounded");
    private readonly int _animIDJump = Animator.StringToHash("Jump");
    private readonly int _animIDFreeFall = Animator.StringToHash("FreeFall");
    private readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

    public ActiveState(Animator animator, CharacterController controller, Camera camera, Transform transform, float gravity,float moveSpeed, 
        float sprintSpeed, float speedChangeRate, float jumpForce, float rotationSmoothTime, float fallTimeout, LayerMask groundLayers)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;
        _groundLayers = groundLayers;

        _gravity = gravity;
        _moveSpeed = moveSpeed;
        _sprintSpeed = sprintSpeed;
        _speedChangeRate = speedChangeRate;
        _jumpForce = jumpForce;
        _rotationSmoothTime = rotationSmoothTime;
        _fallTimeout = fallTimeout;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Stealth", false);
    }

    public override void OnUpdate()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void Move()
    {
        Vector2 input = InputManager.Instance.MoveInputNormalized;
        float targetSpeed = InputManager.Instance.IsSprint ? _sprintSpeed : _moveSpeed;

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

    private void JumpAndGravity()
    {
        if (_isGrounded)
        {
            _fallTimeoutDelta = _fallTimeout;

            if (_animator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _jumpTimeoutDelta <= 0.0f && _isGrounded)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravity);

                if (_animator)
                {
                    _animator.SetBool(_animIDJump, true);
                }
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (_animator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            //_input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
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
}
