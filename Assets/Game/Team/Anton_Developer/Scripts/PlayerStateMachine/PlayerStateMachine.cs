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
        State idleState = new PlayerIdleState(_animator);
        State walkState = new PlayerWalkState(_animator, _playerController, _mainCamera, transform, _walkSpeed, _rotationSpeed);
        State runState = new PlayerRunState(_animator, _playerController, _mainCamera, transform, _runSpeed, _rotationSpeed);

        idleState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !IsSprint())));
        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && IsSprint())));

        walkState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        walkState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => IsMoving() && IsSprint())));

        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !IsMoving())));
        runState.AddTransition(new StateTransition(walkState, new FuncStateCondition(() => IsMoving() && !IsSprint())));

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
}  
