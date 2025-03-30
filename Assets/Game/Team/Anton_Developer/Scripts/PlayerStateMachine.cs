using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    private CharacterController _controller;
    public float Speed { get; set; } = 5f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        ChangeState(new NormalState()); // ������������� ��������� ���������
    }

    void Update()
    {
        _currentState?.Update();
        Move();
    }

    public void ChangeState(IPlayerState newState)
    {
        _currentState?.Exit(); // �������� ����� �� �������� ���������
        _currentState = newState; // ������ ���������
        _currentState.Enter(this); // ������ � ����� ���������
    }

    private void Move()
    {
        if (_currentState is CaughtState) return; // ���� �������, �� ���������

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        _controller.Move(move * Speed * Time.deltaTime);
    }
}
