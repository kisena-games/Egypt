
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<Transform> _waypoints;

    private NavMeshAgent _agent;
    private StateMachine _stateMachine;

    private bool _isIdle;
    private bool _isPatrolling;


    private void Awake()
    {
        _agent=GetComponent<NavMeshAgent>();

        _waypoints = GetComponentsInChildren<Transform>()
            .Where(t => t.name.Contains("Sphere"))
            .Select(waypoint => { waypoint.SetParent(null, true); return waypoint; })
            .ToList();

        InitializeStateMachine();
    }
   
    private void InitializeStateMachine()
    {
        State idleState = new MummyIdleState(_animator,_agent);
        State patrollingState = new MummyPatrollingState(_animator,_agent,_waypoints);
        

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isPatrolling ))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isIdle))); // Заменено на логику с лучом

        _stateMachine= new StateMachine(idleState);
        StartCoroutine(MummyPatrollingState.NavMeshAgentReleaseation());
    }
    private void Update()
    {
        _stateMachine.OnUpdate();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = true;
            _isIdle = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCapsule>())
        {
            _isPatrolling = false;
            _isIdle = true;
        }
    }
}
        
    
    
