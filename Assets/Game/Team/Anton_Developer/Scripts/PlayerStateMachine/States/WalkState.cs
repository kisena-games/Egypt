using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 2f;
        _player.SetAnimation("Walk");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.ChangeState(new RunState());
        }
        else if (_player.Velocity.magnitude < 0.1f) 
        {
            _player.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния: Стелс-режим");
    }
}
