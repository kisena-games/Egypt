public interface ITransition
{
    State NextState { get; }
    ICondition Condition { get; }
    void OnEnter();
    void OnExit();
    void OnUpdate();
}
