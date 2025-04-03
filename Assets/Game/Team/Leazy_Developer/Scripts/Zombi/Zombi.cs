using UnityEngine;

public class Zombi : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _animator;

    private Camera _mainCamera;
    private CharacterController _zombiController;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _zombiController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = InputManager.Instance.MoveInput;
        if (moveInput != Vector2.zero)
        {
            Vector3 camForward = _mainCamera.transform.forward;
            Vector3 camRight = _mainCamera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;
            moveDirection.Normalize();

            _zombiController.Move(moveDirection * _walkSpeed * Time.deltaTime);
        }
    }
}
