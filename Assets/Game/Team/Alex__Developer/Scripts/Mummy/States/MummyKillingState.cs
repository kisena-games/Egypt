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
    private readonly Collider _mummyCollider;

    private int _sceneIndex;
    private float _timeToKill=1.5f;
    private MonoBehaviour _monoBehaviour;

    public MummyKillingState(Animator animator, NavMeshAgent agent,Collider mummyCollider)
    {
        _animator = animator;
        _agent = agent;
        _mummyCollider = mummyCollider;
    }

    public override void OnEnter()
    {
        _agent.isStopped = true;
        _animator.SetBool(KILL_ANIM_KEY, true);
        _mummyCollider.enabled = false;
        PlayerHealth.healthCount--;

    }
   
    public override void OnUpdate()
    {
        _timeToKill -= Time.deltaTime;
        if(_timeToKill < 0)
        {
            PlayerHealth.healthCount--;
            _timeToKill = 1.5f;
        } 
        
    }
    public override void OnExit()
    {
        _agent.isStopped = false;
        _mummyCollider.enabled=true;
        _animator.SetBool(KILL_ANIM_KEY, false);
        _timeToKill = 1.5f;
    }
}
