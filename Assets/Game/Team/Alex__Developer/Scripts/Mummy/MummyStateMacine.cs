
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MummyStateMachine : MonoBehaviour
{
    [Header("Main Mummy Parameters")]
    [SerializeField] private Animator _animator;

    [Header("Patrolling Parameters")]
    [SerializeField] private Transform _patrollingWay;

    [Header("For Attack")]
    [SerializeField] private Transform _player;

    private NavMeshAgent _agent;
    private StateMachine _stateMachine;
    private Transform[] _patrollingPoints;

    private bool _isFeelPlayerSmell = false;
    private bool _isFeelPlayerNoise = false;
    private bool _isFeelPlayerSense = false;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _patrollingPoints = _patrollingWay.GetComponentsInChildren<Transform>();

        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }

    private void InitializeStateMachine()
    {
        State idleState = new MummyIdleState(_animator);
        State patrollingState = new MummyPatrollingState(_animator, _agent, _patrollingPoints);
        State attackState = new MummyAttackState(_animator, _agent, _player);


        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => _isFeelPlayerSmell)));
        patrollingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => !_isFeelPlayerSmell)));
        patrollingState.AddTransition(new StateTransition(attackState, new FuncStateCondition(() => _isFeelPlayerNoise)));
        attackState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => !_isFeelPlayerNoise)));

        _stateMachine = new StateMachine(idleState);
    }

    public void SetSmell(bool isFeelPlayerSmell)
    {
        Debug.Log("SetSmell: " + isFeelPlayerSmell.ToString());

        _isFeelPlayerSmell = isFeelPlayerSmell;
    }
    public void SetNoise(bool isFeelPlayerNoise)
    {
        Debug.Log("SetSmell: " + isFeelPlayerNoise.ToString());
        _isFeelPlayerNoise = isFeelPlayerNoise;
    }
}
        
    
    
