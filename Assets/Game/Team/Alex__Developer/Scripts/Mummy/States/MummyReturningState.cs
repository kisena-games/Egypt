using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MummyReturningState : State
{
    private const string WALK_ANIM_KEY = "Walk";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform _startPoint;

    public MummyReturningState(Animator animator, NavMeshAgent agent, Transform startPoint)
    {
        _animator = animator;
        _agent = agent;
        _startPoint = startPoint;
    }

    public override void OnEnter()
    {
        _agent.isStopped = false;
        _animator.SetBool(WALK_ANIM_KEY, true);
        
    }
    public override void OnUpdate()
    {

        _agent.SetDestination(_startPoint.localPosition);
        Debug.Log("w");
        if (Vector3.Distance(_agent.transform.localPosition, _startPoint.localPosition) <0.5f)
        {
            _animator.SetBool(WALK_ANIM_KEY, false);
            _agent.isStopped = true;
            OnExit();
        }
        
    }

    public override void OnExit()
    {
       
    }
}
