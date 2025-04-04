using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Animator _animator;
    private CharacterController _playerController;
    private Camera _mainCamera;
    private StateMachine _stateMachine;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        State idleState = new IdleState(_animator, _playerController);
        State walkState = new WalkState(_animator, _playerController, _mainCamera, transform, _walkSpeed, _rotationSpeed);
        State runState = new RunState(_animator, _playerController, _mainCamera, transform, _runSpeed, _rotationSpeed);

        // Переходы с использованием FuncCondition
        idleState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !Input.GetKey(KeyCode.LeftShift))));
        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && Input.GetKey(KeyCode.LeftShift))));

        walkState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        walkState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && Input.GetKey(KeyCode.LeftShift))));

        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        runState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !Input.GetKey(KeyCode.LeftShift))));

        _stateMachine = new StateMachine(idleState);
    }

    private bool IsMoving()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        return move.magnitude >= 0.1f;
    }
}
