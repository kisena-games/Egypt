using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    private CharacterController _controller;
    public float Speed { get; set; } = 5f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        ChangeState(new NormalState()); // Устанавливаем стартовое состояние
    }

    void Update()
    {
        _currentState?.Update();
        Move();
    }

    public void ChangeState(IPlayerState newState)
    {
        _currentState?.Exit(); // Вызываем выход из текущего состояния
        _currentState = newState; // Меняем состояние
        _currentState.Enter(this); // Входим в новое состояние
    }

    private void Move()
    {
        if (_currentState is CaughtState) return; // Если пойманы, не двигаемся

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        _controller.Move(move * Speed * Time.deltaTime);
    }
}
