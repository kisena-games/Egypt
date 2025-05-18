using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class MummyReturningState : State
{
    private const string WALK_ANIM_KEY = "Walk";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform[] _patrollingPoints;

    public MummyReturningState(Animator animator, NavMeshAgent agent, Transform[] patrollingPoints)
    {
        _animator = animator;
        _agent = agent;
        _patrollingPoints = patrollingPoints;
    }

    public override void OnEnter()
    {
        Transform closestPoint = null;

        float minDistance = Mathf.Infinity;
        foreach (Transform point in _patrollingPoints)
        {
            float distance = Vector3.Distance(_agent.transform.position, point.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        _animator.SetBool(WALK_ANIM_KEY, true);
        _agent.destination = closestPoint.position;
    }

    public override void OnExit()
    {
        _animator.SetBool(WALK_ANIM_KEY, false);
    }
}
