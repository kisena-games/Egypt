using System;
using UnityEngine;
using UnityEngine.AI;

public class MummyAttackState : State
{
    public static Action OnLook;

    private const string RUN_ANIM_KEY = "Run";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform _player;
    private float _currentSpeed=5f;


    public MummyAttackState(Animator animator, NavMeshAgent agent, Transform player)
    {
        _animator = animator;
        _agent = agent;
        _player = player;

    }

    public override void OnEnter()
    {
        OnLook?.Invoke();
        _agent.speed= _currentSpeed;
        //_animator.speed= _currentSpeed * 2F;
        _animator.SetBool(RUN_ANIM_KEY, true);
        _agent.isStopped = false;
        GoToNextDestination();
    }

    public override void OnExit()
    {
        _animator.SetBool(RUN_ANIM_KEY, false);
    }

    public override void OnUpdate()
    {
        GoToNextDestination();
    }

    private void GoToNextDestination()
    {
        _agent.SetDestination(_player.position);
    }
}

