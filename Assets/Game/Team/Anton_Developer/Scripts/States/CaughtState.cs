using UnityEngine;

public class CaughtState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 0f;
        Debug.Log("Персонажа поймали!");
    }

    public void Update()
    {
        // В этом состоянии нельзя двигаться
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния: Пойман");
    }
}
