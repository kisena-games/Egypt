using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine _player;

    public void Enter(PlayerStateMachine player)
    {
        _player = player;
        _player.Speed = 0f; // ������������� ���������
        Debug.Log("�������� � ������ ��������");
    }

    public void Update()
    {
        // ��������� ���� �������� (A, D, W, S ��� �������)
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _player.ChangeState(new WalkState()); // ���� ����� Shift ? WalkState
            }
            else
            {
                _player.ChangeState(new RunState()); // ������� ��������
            }
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ���������: ��������");
    }
}
