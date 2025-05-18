using UnityEngine;

public class DrawWireSphere : MonoBehaviour
{
    [SerializeField] private float _radius = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
