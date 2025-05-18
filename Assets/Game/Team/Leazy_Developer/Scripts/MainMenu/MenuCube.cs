using System.Collections;
using UnityEngine;

public class MenuCube : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    private Vector3 _targetRotation;

    void Start()
    {
        SetNewTargetRotation();
    }

    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(_targetRotation),
            _rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(_targetRotation)) < 0.1f)
        {
            SetNewTargetRotation();
        }
    }

    private void SetNewTargetRotation()
    {
        int axis = Random.Range(0, 3);

        float angle = Random.Range(0, 2) == 0 ? 90f : -90f;

        if (axis == 0) _targetRotation = new Vector3(angle, 0, 0); // Вращение по X
        else if (axis == 1) _targetRotation = new Vector3(0, angle, 0); // Вращение по Y
        else if (axis == 2) _targetRotation = new Vector3(0, 0, angle); // Вращение по Z
    }
}
