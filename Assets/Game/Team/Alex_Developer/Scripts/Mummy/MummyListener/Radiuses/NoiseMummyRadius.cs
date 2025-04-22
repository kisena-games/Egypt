using UnityEngine;

public class NoiseMummyRadius : MummyRadius
{
    protected override void SetGizmosColor()
    {
        Gizmos.color = Color.yellow;
    }

    protected override void OnTriggerRadiusEnter()
    {
        _mummyController.TriggerNoiseRadiusEnter();
    }

    protected override void OnTriggerRadiusExit()
    {
        _mummyController.TriggerNoiseRadiusExit();
    }
}
