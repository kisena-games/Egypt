using UnityEngine;

public class SmellMummyRadius : MummyRadius
{
    protected override void SetGizmosColor()
    {
        Gizmos.color = Color.green;
    }

    protected override void OnTriggerRadiusEnter()
    {
        _mummyController.TriggerSmellRadiusEnter();
    }

    protected override void OnTriggerRadiusExit()
    {
        _mummyController.TriggerSmellRadiusExit();
    }
}
