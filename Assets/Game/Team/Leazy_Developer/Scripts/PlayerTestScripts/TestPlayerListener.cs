using UnityEngine;

public class TestPlayerListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IMummyRadius mummyRadius))
        {
            mummyRadius.TriggerRadiusEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IMummyRadius mummyRadius))
        {
            mummyRadius.TriggerRadiusExit();
        }
    }
}
