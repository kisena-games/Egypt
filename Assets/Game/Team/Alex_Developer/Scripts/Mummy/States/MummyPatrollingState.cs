using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.AI;

public class MummyPatrollingState:State
{
    private static List<Transform> _waypoints;

    private readonly Animator _animator;

    private static NavMeshAgent _agent; 
    
    private static bool _isBreak=true;

    private static float _timer,_currentSpeed;

    public MummyPatrollingState(Animator animator, NavMeshAgent agent, List<Transform> waypoints)
    {
        _animator = animator;
        _agent = agent;
        _waypoints = waypoints;
        _currentSpeed = _agent.speed;
    }
    
    public override void OnEnter()
    {
        _isBreak = false;
        
        Debug.Log("Patrolling");
        _animator.SetBool("Patrolling", true);
        _agent.speed = _currentSpeed;
    }

    public override void OnExit()
    {
        _isBreak = true;
        _animator.SetBool("Patrolling", false);
        _agent.speed = 0f;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if ((int)_timer % 2 == 0)
        {
            _agent.speed = _currentSpeed;
        }
        else
        {
            _agent.speed = 0f;
        }

    }
    public static IEnumerator NavMeshAgentReleaseation()
    {
        while (true)
        {
            if (_isBreak==false)
            {
                var waypoint = _waypoints[Random.Range(0, _waypoints.Count)].position;
                _agent.SetDestination(waypoint);
      
                yield return new WaitForSeconds(Random.Range(4, 6));
            }
            yield return null;
        }
    }
}

