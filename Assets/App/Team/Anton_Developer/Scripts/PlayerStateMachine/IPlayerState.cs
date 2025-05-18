public interface IPlayerState
{
    void Enter(PlayerStateMachine player); // Вызывается при входе в состояние
    void Update(); // Логика обновления состояния
    void Exit(); // Вызывается при выходе из состояния
}