using System;
using UnityEngine;
using UnityEngine.AI;

public class MummyPatrollingState:State
{
    public static Action OnMummyWalk;
    public static Action OnAnubisWalk;


    private const string WALK_ANIM_KEY = "Walk";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform[] _patrollingPoints;
    private float _timer;
    private float _currentSpeed=1f;

    private int _pointIndex;
    private bool _isAnubis;

    private float _timeForStep;

    public MummyPatrollingState(Animator animator, NavMeshAgent agent, Transform[] patrollingPoints,bool isAnubis)
    {
        _animator = animator;
        _agent = agent;
        _patrollingPoints = patrollingPoints;
        _isAnubis = isAnubis;
    }
    
    public override void OnEnter()
    {
        _agent.speed = _currentSpeed;
        _animator.speed = _currentSpeed*2F;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < _patrollingPoints.Length; i++)
        {
            float distance = Vector3.Distance(_agent.transform.position, _patrollingPoints[i].position);

            if (distance < minDistance)
            {
                minDistance = distance;
                _pointIndex = i;
            }
        }

        _animator.SetBool(WALK_ANIM_KEY, true);

        _agent.isStopped = false;
        GoToNextDestination();
    }

    public override void OnExit()
    {
        //_animator.SetBool(WALK_ANIM_KEY, false);
        //_agent.isStopped = true;
    }

    public override void OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(_agent.transform.position, _agent.destination);

        if (distanceToTarget <= _agent.stoppingDistance)
        {
            GoToNextDestination();
        }
        void ForAudio()
        {
            _timeForStep += Time.deltaTime;
            if (_timeForStep > 1f)
            {
                if (_isAnubis)
                    OnAnubisWalk?.Invoke();
                else
                    OnMummyWalk?.Invoke();

                _timeForStep = 0;
            }
        }
        ForAudio();
    }

    private void GoToNextDestination()
    {
        _agent.SetDestination(_patrollingPoints[_pointIndex].position);
        _pointIndex = _pointIndex >= _patrollingPoints.Length - 1 ? 0 : _pointIndex + 1;
    }
}

