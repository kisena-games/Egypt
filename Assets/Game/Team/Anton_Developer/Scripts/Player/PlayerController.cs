using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const string IDLE_ANIM_KEY = "Idle";
    private const string WALK_ANIM_KEY = "Walk";
    private const string RUN_ANIM_KEY = "Run";
    private const string JUMP_ANIM_KEY = "Jump";

    [Header("Player Settings")]
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float rotationSmoothTime = 0.12f;
    public float speedChangeRate = 10.0f;

    [Header("Jump Settings")]
    public float jumpHeight = 1.2f;
    public float gravity = -15.0f;
    public float jumpTimeout = 0.5f;
    public float fallTimeout = 0.15f;

    [Header("Ground Check")]
    public bool grounded = true;
    public float groundedOffset = -0.14f;
    public float groundedRadius = 0.28f;
    public LayerMask groundLayers;

    private CharacterController _controller;
    private Camera _mainCamera;
    private Animator _animator;
    private float _speed;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
        _mainCamera = Camera.main;
    }

    void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        Move();
        UpdateAnimations();
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    void Move()
    {
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if (targetSpeed > 0 && currentHorizontalSpeed < 0.1f)
        {
            _speed = targetSpeed;
        }
        else if (Mathf.Abs(currentHorizontalSpeed - targetSpeed) > speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(inputX, 0.0f, inputZ);

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 camForward = _mainCamera.transform.forward;
            Vector3 camRight = _mainCamera.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * inputZ + camRight * inputX;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSmoothTime);

            _controller.Move(moveDirection.normalized * (_speed * Time.deltaTime) + Vector3.up * _verticalVelocity * Time.deltaTime);
        }
        else
        {
            _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }
    }

    void JumpAndGravity()
    {
        if (grounded)
        {
            _fallTimeoutDelta = fallTimeout;

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                _animator?.SetBool(JUMP_ANIM_KEY, true);
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    void UpdateAnimations()
    {
        bool isMoving = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).magnitude > 0.1f;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        bool isJumping = !grounded;

        _animator.SetBool(IDLE_ANIM_KEY, !isMoving && !isJumping);
        _animator.SetBool(WALK_ANIM_KEY, isMoving && !isRunning && grounded);
        _animator.SetBool(RUN_ANIM_KEY, isRunning && grounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = grounded ? new Color(0, 1, 0, 0.35f) : new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }
}