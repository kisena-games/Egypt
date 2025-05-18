namespace App.Scripts.Bootstrap.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}