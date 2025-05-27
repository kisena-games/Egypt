namespace App.Scripts.Bootstrap.StateMachine
{
    public interface IPayloadState<in TPayload> : IExitState
    {
        void Enter(TPayload payload);
    }
}