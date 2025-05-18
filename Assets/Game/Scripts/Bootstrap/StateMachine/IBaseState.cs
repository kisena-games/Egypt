namespace Game.Scripts.Infrastructure.StateMachine
{
    public interface IState
    {
        public void Exit();
        public void Enter();
    }
}