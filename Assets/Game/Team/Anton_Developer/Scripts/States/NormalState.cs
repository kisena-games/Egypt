using UnityEngine;

public class NormalState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 5f;
        Debug.Log("����� � ���������: ������� ������������");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _player.ChangeState(new StealthState());
        }
        if (Input.GetKeyDown(KeyCode.Space)) // ��������� �������
        {
            _player.ChangeState(new CaughtState());
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ���������: ������� ������������");
    }
}
