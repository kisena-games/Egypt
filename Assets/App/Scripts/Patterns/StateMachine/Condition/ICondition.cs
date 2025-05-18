public interface ICondition
{
    bool IsConditionSuccess();
    void OnEnter();
    void OnExit();
    void OnUpdate();
}
