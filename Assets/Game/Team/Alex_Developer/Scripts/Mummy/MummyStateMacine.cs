
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MummyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Light _spotLight;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _detectionDistance = 4f;
    [SerializeField] private float _viewAngle = 120f;
    [SerializeField] private List<Vector3> _waypoints;

    private StateMachine _stateMachine;
    private Camera _mainCamera;
    private NavMeshAgent _agent;

    private Vector2 _startPosition;

    private bool _isIdle;
    private bool _isPatrolling;
    float _timer;


    private void Awake()
    {
        _mainCamera=Camera.main;
        _agent=GetComponent<NavMeshAgent>();
        _startPosition=transform.position;

        InitializeStateMachine();
        StartCoroutine(NavMeshAgentReleaseation());
 
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        NavMeshAgentReleaseation();
    }

    private void InitializeStateMachine()
    {
        State idleState = new MummyIdleState(_animator);
        State patrollingState = new MummyPatrollingState(_animator);

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isPatrolling))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isIdle))); // Заменено на логику с лучом

        _stateMachine = new StateMachine(idleState);
    }
    private IEnumerator NavMeshAgentReleaseation()
    {
        while (true)
        {
            _agent.SetDestination(_waypoints[Random.Range(0,_waypoints.Count)]);
            yield return new WaitForSeconds(Random.Range(1,3));
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = false;
            _isIdle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = true;
            _isIdle = false;
        }
    }
}
        
    
    
