
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
    [Header("For loose in level")]
    [SerializeField] private int _sceneIndex;

    private NavMeshAgent _agent;
    private StateMachine _stateMachine;
    private Transform[] _patrollingPoints;

    public bool isFeelPlayerSmell { get; private set; }
    public bool isFeelPlayerNoise { get; private set; }
    public bool isFeelPlayerKill { get; private set; }

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
        State killState = new MummyKillingState(_animator, _agent);


        idleState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => isFeelPlayerSmell)));

        patrollingState.AddTransition(new StateTransition(attackState, new FuncStateCondition(() => isFeelPlayerNoise)));
        attackState.AddTransition(new StateTransition(patrollingState, new FuncStateCondition(() => !isFeelPlayerNoise)));
        attackState.AddTransition(new StateTransition(killState, new FuncStateCondition(() => isFeelPlayerKill)));
        killState.AddTransition(new StateTransition(attackState, new FuncStateCondition(() => !isFeelPlayerKill)));
        _stateMachine = new StateMachine(idleState);
    }

    public void SetSmell(bool isFeelPlayerSmell)
    {
        Debug.Log("SetSmell: " + isFeelPlayerSmell.ToString());

        this.isFeelPlayerSmell = isFeelPlayerSmell;
 
    }
    public void SetNoise(bool isFeelPlayerNoise)
    {
        Debug.Log("SetNoise: " + isFeelPlayerNoise.ToString());
        this.isFeelPlayerNoise = isFeelPlayerNoise;
    }
    public void SetKill(bool isFeelPlayerKill)
    {
        Debug.Log("SetKill: " + isFeelPlayerKill.ToString());
        this.isFeelPlayerKill = isFeelPlayerKill;
    }
    
}
        
    
    
