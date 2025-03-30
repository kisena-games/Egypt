using UnityEngine;

public class CaughtState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 0f;
        Debug.Log("��������� �������!");
    }

    public void Update()
    {
        // � ���� ��������� ������ ���������
    }

    public void Exit()
    {
        Debug.Log("����� �� ���������: ������");
    }
}
