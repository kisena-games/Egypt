using UnityEngine;

public class NormalState : IPlayerState
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
            _player.ChangeState(new StealthState());
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Симуляция захвата
        {
            _player.ChangeState(new CaughtState());
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния: Обычное передвижение");
    }
}
