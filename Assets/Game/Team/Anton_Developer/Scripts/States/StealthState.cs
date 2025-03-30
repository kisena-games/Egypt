using UnityEngine;

public class StealthState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 2f;
        Debug.Log("����� � ���������: �����-�����");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.ChangeState(new NormalState());
        }
        if (Input.GetKeyDown(KeyCode.Space)) // ��������� �������
        {
            _player.ChangeState(new CaughtState());
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ���������: �����-�����");
    }
}
