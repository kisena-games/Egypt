using UnityEngine;

public class RunState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 5f;
        Debug.Log("Вошел в состояние: Обычное передвижение");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _player.ChangeState(new WalkState());
        }
        else if (_player.Velocity.magnitude < 0.1f) // Проверяем скорость
        {
            _player.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния: Обычное передвижение");
    }
}
