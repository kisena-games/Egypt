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

    public MummyPatrollingState(Animator animator, NavMeshAgent agent, List<Transform> waypoints)
    {
        _animator = animator;
        _agent = agent;
        _waypoints = waypoints;
    }
    
    public override void OnEnter()
    {
        _isBreak = false;
        
        Debug.Log("Patrolling");
    }

    public override void OnExit()
    {
        _isBreak = true;
    }

    public override void OnUpdate()
    {
        if(_agent.velocity.magnitude<1) _animator.SetBool("Patrolling", false); 
        else _animator.SetBool("Patrolling", true);
    }
    public static IEnumerator NavMeshAgentReleaseation()
    {
        while (true)
        {
            if (_isBreak==false)
            {
                var waypoint = _waypoints[Random.Range(0, _waypoints.Count)].position;
                _agent.SetDestination(waypoint);
                _agent.transform.LookAt(new Vector3(0, waypoint.y, 0));
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
            yield return null;
        }
        yield return null;
    }
}

