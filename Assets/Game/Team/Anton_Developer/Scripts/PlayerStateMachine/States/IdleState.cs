using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 0f; // Останавливаем персонажа
        _player.SetAnimation("Idle");
    }

    public void Update()
    {
        // Проверяем ввод движения (A, D, W, S или стрелки)
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _player.ChangeState(new WalkState()); // Если зажат Shift ? WalkState
            }
            else
            {
                _player.ChangeState(new RunState()); // Обычное движение
            }
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния: Ожидание");
    }
}
