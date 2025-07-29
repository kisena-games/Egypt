using UnityEngine;
using EventWwise = AK.Wwise.Event;

public class MummyAudio : MonoBehaviour
{
    public EventWwise
       wwiseOnAnubisLook, wwiseOnMummyLook,
       wwiseOnAnubisBit, wwiseOnMummyBit,
       wwiseOnAnubisWalk, wwiseOnMummyWalk,
        wwiseOnAnubisRun, wwiseOnMummyRun;
   

    private void OnEnable()
    {
        MummyPatrollingState.OnAnubisWalk += OnAnubisWalk;
        MummyPatrollingState.OnMummyWalk += OnMummyWalk;
        MummyAttackState.OnAnubisLook += OnAnubisLook;
        MummyAttackState.OnMummyLook += OnMummyLook;
        MummyAttackState.OnAnubisRun += OnAnubisRun;
        MummyAttackState.OnMummyRun += OnMummyRun;
        MummyKillingState.OnAnubisBit += OnAnubisBit;
        MummyKillingState.OnMummyBit += OnMummyBit;

    }
    private void OnDisable()
    {
        MummyPatrollingState.OnAnubisWalk -= OnAnubisWalk;
        MummyPatrollingState.OnMummyWalk -= OnMummyWalk;
        MummyAttackState.OnAnubisRun -= OnAnubisRun;
        MummyAttackState.OnMummyRun -= OnMummyRun;
        MummyAttackState.OnAnubisLook -= OnAnubisLook;
        MummyAttackState.OnMummyLook -= OnMummyLook;
        MummyKillingState.OnAnubisBit -= OnAnubisBit;
        MummyKillingState.OnMummyBit -= OnMummyBit;
    }
    private void OnAnubisLook()
    {
        wwiseOnAnubisLook.Post(gameObject);
    }
    private void OnMummyLook()
    {
        wwiseOnMummyLook.Post(gameObject);

    }
    private void OnMummyBit()
    {
        wwiseOnAnubisBit.Post(gameObject);
    }
    private void OnAnubisBit()
    {
        wwiseOnMummyBit.Post(gameObject);
    }
    private void OnAnubisRun()
    {
        wwiseOnAnubisRun.Post(gameObject);
    }
    private void OnMummyRun()
    {
        wwiseOnMummyRun.Post(gameObject);
    }
    private void OnAnubisWalk()
    {
        wwiseOnAnubisWalk.Post(gameObject);
    }
    private void OnMummyWalk()
    {
        wwiseOnMummyWalk.Post(gameObject);
    }

}
