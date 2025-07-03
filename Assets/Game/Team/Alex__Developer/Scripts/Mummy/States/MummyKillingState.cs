using System.Collections;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class MummyKillingState : State
{
    private const string KILL_ANIM_KEY = "Kill";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private int _sceneIndex;
    private float _timeToKill=1f;

    public MummyKillingState(Animator animator, NavMeshAgent agent,int sceneIndex)
    {
        _animator = animator;
        _agent = agent;
        _sceneIndex = sceneIndex;
    }

    public override void OnEnter()
    {
        _agent.isStopped = true;
        _animator.SetBool(KILL_ANIM_KEY, true);
        
    }
    public override void OnUpdate()
    {
        _timeToKill -= Time.deltaTime;
        if(_timeToKill < 0 ) 
        SceneManager.LoadScene(_sceneIndex);
    }
    public override void OnExit()
    {
        
    }
}
