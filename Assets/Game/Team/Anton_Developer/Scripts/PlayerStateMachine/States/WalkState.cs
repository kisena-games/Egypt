using UnityEngine;

public class WalkState : State
{
    private readonly Animator _animator;
    private readonly CharacterController _controller;
    private readonly Camera _camera;
    private readonly Transform _transform;
    private readonly float _walkSpeed;
    private readonly float _rotationSpeed;

    public WalkState(Animator animator, CharacterController controller, Camera camera, Transform transform, float walkSpeed, float rotationSpeed)
    {
        _animator = animator;
        _controller = controller;
        _camera = camera;
        _transform = transform;
        _walkSpeed = walkSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("Idle", false);
        _animator.SetBool("Run", false);
    }

    public override void OnUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camRight = _camera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * moveZ + camRight * moveX;
            moveDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);

            _controller.Move(moveDirection * _walkSpeed * Time.deltaTime);
        }
    }
}
