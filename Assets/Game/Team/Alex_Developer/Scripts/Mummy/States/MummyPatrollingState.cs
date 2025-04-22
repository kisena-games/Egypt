using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.AI;

public class MummyPatrollingState:State
{
    private const string WALK_ANIM_KEY = "Walk";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform[] _patrollingPoints;
    private float _timer;
    private float _currentSpeed;

    private int _pointIndex;

    public MummyPatrollingState(Animator animator, NavMeshAgent agent, Transform[] patrollingPoints)
    {
        _animator = animator;
        _agent = agent;
        _patrollingPoints = patrollingPoints;
        _currentSpeed = _agent.speed;
    }
    
    public override void OnEnter()
    {
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
        _animator.SetBool(WALK_ANIM_KEY, false);
        _agent.isStopped = true;
    }

    public override void OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(_agent.transform.position, _agent.destination);

        if (distanceToTarget <= _agent.stoppingDistance)
        {
            GoToNextDestination();
        }

    }

    private void GoToNextDestination()
    {
        GameObject.Instantiate(new GameObject(), _patrollingPoints[_pointIndex]);
        _agent.SetDestination(_patrollingPoints[_pointIndex].position);
        _pointIndex = _pointIndex >= _patrollingPoints.Length - 1 ? 0 : _pointIndex + 1;
    }
}

