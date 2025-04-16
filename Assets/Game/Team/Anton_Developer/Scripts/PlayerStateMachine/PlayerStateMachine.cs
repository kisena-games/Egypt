using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravity = 9.81f;

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
        State idleState = new PlayerIdleState(_animator, _playerController, _gravity);
        State walkState = new PlayerWalkState(_animator, _playerController, _mainCamera, transform, _walkSpeed, _rotationSpeed, _gravity);
        State runState = new PlayerRunState(_animator, _playerController, _mainCamera, transform, _runSpeed, _rotationSpeed, _gravity);
        State jumpState = new PlayerJumpState(_animator, _playerController, _mainCamera, transform, _jumpForce, _rotationSpeed, _gravity);

        idleState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !IsSprint())));
        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && IsSprint())));
        idleState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => IsJumping())));

        walkState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        walkState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && IsSprint())));
        walkState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => IsJumping())));

        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        runState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !IsSprint())));
        runState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => IsJumping())));

        jumpState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _playerController.isGrounded && !IsMoving())));
        jumpState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => _playerController.isGrounded && IsMoving() && !IsSprint())));
        jumpState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _playerController.isGrounded && IsMoving() && IsSprint())));

        _stateMachine = new StateMachine(idleState);
    }

    private bool IsMoving()
    {
        return InputManager.Instance.IsMoving;
    }

    private bool IsSprint()
    {
        return InputManager.Instance.IsSprint;
    }

    private bool IsJumping()
    {
        bool isGrounded = _playerController.isGrounded;
        return Input.GetKeyDown(KeyCode.Space) && isGrounded;
    }
}
