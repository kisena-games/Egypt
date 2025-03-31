using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    private CharacterController _controller;
    public float Speed { get; set; } = 5f;
    public Vector3 Velocity => _controller.velocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        ChangeState(new IdleState()); // ������������� ��������� ���������
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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        _controller.Move(move * Speed * Time.deltaTime);
    }
}
