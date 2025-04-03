using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Animator _animator;
    private CharacterController _playerController;
    private Camera _mainCamera;
    private float _currentSpeed;
    private string _currentState = "Idle"; // Храним текущее состояние

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            Vector3 camForward = _mainCamera.transform.forward;
            Vector3 camRight = _mainCamera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * moveZ + camRight * moveX;
            moveDirection.Normalize();

            // Проверяем, зажат ли Shift
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;

            // Применяем движение и поворот
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);

            _playerController.Move(moveDirection * targetSpeed * Time.deltaTime);

            _currentSpeed = targetSpeed;
        }
        else
        {
            _currentSpeed = 0f;
        }

        if (_currentSpeed == 0 && _currentState != "Idle")
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", false);
            _animator.SetBool("Idle", true); // Теперь точно включится Idle
            _currentState = "Idle";
        }
        else if (_currentSpeed > 0 && _currentSpeed <= _walkSpeed && _currentState != "Walk")
        {
            _animator.SetBool("Walk", true);
            _animator.SetBool("Run", false);
            _animator.SetBool("Idle", false);
            _currentState = "Walk";
        }
        else if (_currentSpeed > _walkSpeed && _currentState != "Run")
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", true);
            _animator.SetBool("Idle", false);
            _currentState = "Run";
        }
    }
}
