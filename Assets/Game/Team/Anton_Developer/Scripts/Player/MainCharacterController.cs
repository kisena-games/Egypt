using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MainCharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _sprintSpeed = 6.5f;
    [SerializeField] private float _speedChangeRate = 10.0f;
    [SerializeField] private float _jumpHeight = 1.2f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _groundedRadius = 0.28f;
    [SerializeField] private float _groundedOffset = -0.14f;
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private float _fallTimeout;
    [SerializeField] private float _jumpTimeout = 0.50f;
    [SerializeField] private LayerMask _groundLayers;
    
    private CharacterController _controller;
    private Camera _mainCamera;
    private Animator _animator;
    private bool _isGrounded = true;
    private float _speed;
    private float _verticalVelocity;
    private float _fallTimeoutDelta;
    private float _jumpTimeoutDelta;
    private float _terminalVelocity = 53.0f;

    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDMotionSpeed;
    private float _animationBlend;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _mainCamera = Camera.main;
        AssignAnimationIDs();
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
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
            Vector3 camForward = _mainCamera.transform.forward;
            Vector3 camRight = _mainCamera.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camRight * input.x + camForward * input.y;
            moveDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSmoothTime);

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
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, 1);
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
            }

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _jumpTimeoutDelta <= 0.0f && _isGrounded)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

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
            _jumpTimeoutDelta = _jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (_animator)
                {
                    _animator.SetBool(_animIDJump, true);
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
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset,
                transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers,
                QueryTriggerInteraction.Ignore);

        if (_animator)
        {
            _animator.SetBool(_animIDGrounded, _isGrounded);
        }
    }
}
