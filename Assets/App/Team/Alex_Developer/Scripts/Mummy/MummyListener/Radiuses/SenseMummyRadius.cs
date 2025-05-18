using UnityEngine;

public class SenseMummyRadius : MummyRadius
{
    protected override void SetGizmosColor()
    {
        Gizmos.color = Color.red;
    }

    protected override void OnTriggerRadiusEnter()
    {
        _mummyController.TriggerSenseRadiusEnter();
    }

    protected override void OnTriggerRadiusExit()
    {
        _mummyController.TriggerSenseRadiusExit();
    }
}
