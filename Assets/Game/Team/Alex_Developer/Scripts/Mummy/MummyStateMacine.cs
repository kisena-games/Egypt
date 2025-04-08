using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MummyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _detectionDistance = 4f;
    [SerializeField] private float _viewAngle = 120f;

    private CharacterController _mummyController;
    private StateMachine _stateMachine; 

    private Transform _player;

    private bool _isIdle;
    private bool _isPatrolling;

    private void Awake()
    {
        _mummyController = GetComponent<CharacterController>();
        _player = FindFirstObjectByType<PlayerCapsule>().transform;

        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();

        DetectPlayer();
    }

    private void InitializeStateMachine()
    {
        State idleState = new MummyIdleState(_animator, _mummyController);
        State patrollingState = new MummyPatrollingState(_animator, _mummyController);

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isPatrolling))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isIdle))); // Заменено на логику с лучом

        _stateMachine = new StateMachine(idleState);
    }

    private void DetectPlayer()
    {
        if (_player != null)
        {
            Vector3 directionToPlayer = (_player.position - transform.position).normalized;

            float angle = Vector3.Angle(transform.position, directionToPlayer);

            if (angle < _viewAngle * 0.5f) 
            {
                if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, _detectionDistance))
                {
                    _isPatrolling = true;
                    _isIdle = false;
                }
                
            }
            else
            {
                _isPatrolling = false;
                _isIdle = true;
            }
        }
    }
}
    
