using System.Collections;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    [SerializeField] private float _timeToCalmDownSmell = 5f;

    private MummyStateMachine _mummyStateMachine;

    private Coroutine _smellCoroutine;

    private void Awake()
    {
        _mummyStateMachine = GetComponent<MummyStateMachine>();
    }

    public void TriggerSmellRadiusEnter()
    {
        Debug.Log("SMELL RADIUS ENTER");

        if (_smellCoroutine != null)
        {
            StopCoroutine(_smellCoroutine);
            _smellCoroutine = null;
        }

        _mummyStateMachine.SetSmell(true);
    }

    public void TriggerSmellRadiusExit()
    {
        Debug.Log("SMELL RADIUS EXIT");

        _smellCoroutine = StartCoroutine(WaitToSmellCalmDown());
    }

    public void TriggerNoiseRadiusEnter()
    {
        Debug.Log("NOISE RADIUS ENTER");
    }

    public void TriggerNoiseRadiusExit()
    {
        Debug.Log("NOISE RADIUS EXIT");
    }

    public void TriggerSenseRadiusEnter()
    {
        Debug.Log("SENSE RADIUS ENTER");
    }

    public void TriggerSenseRadiusExit()
    {
        Debug.Log("SENSE RADIUS EXIT");
    }

    IEnumerator WaitToSmellCalmDown()
    {
        yield return new WaitForSeconds(_timeToCalmDownSmell);

        _mummyStateMachine.SetSmell(false);
    }
}
