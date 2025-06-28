using UnityEngine;
using UnityEngine.AI;

public class MummyAttackState : State
{
    private const string WALK_ANIM_KEY = "Walk";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform _player;
    private float _currentSpeed=3f;


    public MummyAttackState(Animator animator, NavMeshAgent agent, Transform player)
    {
        _animator = animator;
        _agent = agent;
        _player = player;

    }

    public override void OnEnter()
    {
        _agent.speed= _currentSpeed;
        _animator.speed= _currentSpeed * 2F;
        _animator.SetBool(WALK_ANIM_KEY, true);
        _agent.isStopped = false;
        GoToNextDestination();
    }

    public override void OnExit()
    {
        
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

