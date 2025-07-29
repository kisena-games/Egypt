using System;
using System.Collections;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class MummyKillingState : State
{
    public static Action OnAnubisBit, OnMummyBit;

    private const string KILL_ANIM_KEY = "Kill";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;
    private readonly Collider _mummyCollider;

    private int _sceneIndex;
    private float _timeToKill=1.5f;
    private bool _isAnubis;
    private MonoBehaviour _monoBehaviour;

    public MummyKillingState(Animator animator, NavMeshAgent agent,Collider mummyCollider,bool isAnubis)
    {
        _animator = animator;
        _agent = agent;
        _mummyCollider = mummyCollider;
        _isAnubis = isAnubis;
    }

    public override void OnEnter()
    {
        BitCall();
        _agent.isStopped = true;
        _animator.SetBool(KILL_ANIM_KEY, true);
        _mummyCollider.enabled = false;
        if(_isAnubis)
        PlayerHealth.healthCount-=2;
        else PlayerHealth.healthCount--;

    }
   
    public override void OnUpdate()
    {
        _timeToKill -= Time.deltaTime;
        if(_timeToKill < 0)
        {
            BitCall();
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
    private void BitCall()
    {
        if (_isAnubis)
            OnAnubisBit?.Invoke();
        else
            OnMummyBit?.Invoke();
    }
}
