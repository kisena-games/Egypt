using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _sprintSpeed = 6.5f;
    [SerializeField] private float _speedChangeRate = 10.0f;
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private float _jumpForce = 1.2f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _fallTimeout;
    [SerializeField] private LayerMask _groundLayers;

    private CharacterController _playerController;
    private Camera _mainCamera;
    private StateMachine _stateMachine;

    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }

    private void InitializeStateMachine()
    {
        State activeState = new ActiveState(_animator, _playerController, _mainCamera, transform, _gravity, _moveSpeed, 
            _sprintSpeed, _speedChangeRate, _jumpForce, _rotationSmoothTime, _fallTimeout, _groundLayers);
        State stealthState = new StealthState(_animator, _playerController, _mainCamera, transform, _moveSpeed,
            _speedChangeRate, _rotationSmoothTime);

        _stateMachine = new StateMachine(activeState);
    }

    private bool IsMoving()
    {
        return InputManager.Instance.IsMoving;
    }
}
