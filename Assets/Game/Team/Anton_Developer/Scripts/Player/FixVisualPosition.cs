using UnityEngine;

public class FixVisualPosition : MonoBehaviour
{
    private Vector3 _initialLocalPosition;

    private void Start()
    {
        // —охран€ем начальную локальную позицию визуальной части
        _initialLocalPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        //  аждый кадр принудительно возвращаем в начальную локальную позицию
        transform.localPosition = _initialLocalPosition;
    }
}
