using UnityEngine;

// Добавьте этот компонент к Mummy
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class MummyStateMachine : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _detectionDistance = 4f;
    [SerializeField] private float _viewAngle = 120f;

    private Animator _animator;
    private CharacterController _mummyController;
    private StateMachine _stateMachine;
    private State idleState, patrollingState;

    private Transform _player;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mummyController = GetComponent<CharacterController>();

        InitializeStateMachine();

        _player = GameObject.Find("PlayerCapsule")?.transform;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();

        DetectPlayer();
    }

    private void InitializeStateMachine()
    {
        idleState = new MummyIdleState(_animator, _mummyController);
        patrollingState = new MummyPatrollingState(_animator, _mummyController);

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => false))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => false))); // Заменено на логику с лучом

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
                    _stateMachine = new StateMachine(patrollingState);
                
                }
                
            }
            else
                _stateMachine = new StateMachine(idleState);
        }
    }
}
    
