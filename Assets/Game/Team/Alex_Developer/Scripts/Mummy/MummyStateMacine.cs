
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
  
        InitializeStateMachine();
        
        _waypoints = new List<Transform>(GetComponentsInChildren<Transform>()
            .Where(t => t.name.Contains("Sphere")));
        MummyPatrollingState.waypoints = _waypoints;
        StartCoroutine(MummyPatrollingState.NavMeshAgentReleaseation());
        foreach (Transform waypoint in _waypoints)
        {
            waypoint.SetParent(null, true);
        }
    }
   
    private void InitializeStateMachine()
    {
        State idleState = new MummyIdleState(_animator,_agent);
        State patrollingState = new MummyPatrollingState(_animator,_agent);
        

        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isPatrolling ))); // Заменено на логику с лучом
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isIdle))); // Заменено на логику с лучом

        _stateMachine= new StateMachine(idleState);
    }
    private void Update()=>_stateMachine.OnUpdate();
    
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
        
    
    
