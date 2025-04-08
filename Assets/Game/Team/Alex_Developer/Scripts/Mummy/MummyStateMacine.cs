using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class MummyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Light _spotLight;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _detectionDistance = 4f;
    [SerializeField] private float _viewAngle = 120f;
    
    private CharacterController _mummyController;
    private StateMachine _stateMachine;

    private bool _isIdle;
    private bool _isPatrolling;

    State idleState;
    State patrollingState;

    private void Awake()
    {
        _mummyController = GetComponent<CharacterController>();
        
        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }

    private void InitializeStateMachine()
    {
        idleState = new MummyIdleState(_animator, _mummyController);
        patrollingState = new MummyPatrollingState(_animator, _mummyController);

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isPatrolling))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isIdle))); // Заменено на логику с лучом

        _stateMachine = new StateMachine(idleState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = false;
            _isIdle = true;
            _stateMachine = new StateMachine(patrollingState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = false;
            _isIdle = true;
            _stateMachine = new StateMachine(idleState);
        }
    }
}
        
    
    
