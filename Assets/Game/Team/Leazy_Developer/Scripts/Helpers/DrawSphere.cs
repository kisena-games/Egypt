using UnityEngine;

public class DrawSphere : MonoBehaviour
{
    [SerializeField] private float _radius = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
