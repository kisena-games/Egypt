using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
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

    // Private variables
    private CharacterController _controller;
    private float _speed;
    private float _verticalVelocity;
    private float _rotationVelocity;
    private float _targetRotation = 0.0f;
    private float _terminalVelocity = 53.0f;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
    }

    void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        Move();
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

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        if (inputDirection != Vector3.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 moveDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(moveDirection.normalized * (_speed * Time.deltaTime) + Vector3.up * _verticalVelocity * Time.deltaTime);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = grounded ? new Color(0, 1, 0, 0.35f) : new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }
}