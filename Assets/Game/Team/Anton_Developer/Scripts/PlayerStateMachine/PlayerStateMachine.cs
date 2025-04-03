using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    private CharacterController _controller;
    private Animator _animator;

    public float Speed { get; set; } = 5f;
    public Vector3 Velocity => _controller.velocity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        ChangeState(new IdleState());
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator не найден на персонаже!");
        }
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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        _controller.Move(move * Speed * Time.deltaTime);

        _animator.SetFloat("Speed", move.magnitude * Speed);
    }

    public void SetAnimation(string animationName)
    {
        _animator.Play(animationName);
    }
}
