using System;
using UnityEngine;
using UnityEngine.AI;

public class MummyAttackState : State
{
    public static Action OnMummyLook,OnAnubisLook;
    public static Action OnMummyRun;
    public static Action OnAnubisRun;

    private const string RUN_ANIM_KEY = "Run";

    private readonly Animator _animator;
    private readonly NavMeshAgent _agent;

    private Transform _player;
    private float _currentSpeed=5f;
    private bool _isAnubis;

    private float _timeForStep;

    public MummyAttackState(Animator animator, NavMeshAgent agent, Transform player,bool isAnubis)
    {
        _animator = animator;
        _agent = agent;
        _player = player;
        _isAnubis = isAnubis;
    }

    public override void OnEnter()
    {
        
        LookCall();
        _agent.speed= _currentSpeed;
        //_animator.speed= _currentSpeed * 2F;
        _animator.SetBool(RUN_ANIM_KEY, true);
        _agent.isStopped = false;
        GoToNextDestination();
    }

    public override void OnExit()
    {
        _animator.SetBool(RUN_ANIM_KEY, false);
    }

    public override void OnUpdate()
    {
        GoToNextDestination();

        void ForAudio()
        {
            _timeForStep += Time.deltaTime;
            if (_timeForStep > 0.3f)
            {
                if (_isAnubis)
                    OnAnubisRun?.Invoke();
                else
                    OnMummyRun?.Invoke();

                _timeForStep = 0;
            }
        }
        ForAudio();
    }

    private void GoToNextDestination()
    {
        _agent.SetDestination(_player.position);
    }
    private void LookCall()
    {
        if (_isAnubis)
            OnAnubisLook?.Invoke();
        else
            OnMummyLook?.Invoke();
    }
}

