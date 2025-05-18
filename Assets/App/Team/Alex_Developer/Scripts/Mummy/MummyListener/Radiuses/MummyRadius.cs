using System;
using UnityEngine;

public abstract class MummyRadius : MonoBehaviour, IMummyRadius
{
    [SerializeField] private float _radius = 1.0f;

    protected MummyController _mummyController;
    private SphereCollider _collider;

    private void OnDrawGizmos()
    {
        SetGizmosColor();
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    protected abstract void SetGizmosColor();

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _mummyController = GetComponentInParent<MummyController>();

        if (_collider.radius != _radius)
        {
            _collider.radius = _radius;
        }
    }

    public void SetRadius(float newRadius)
    {
        _radius = newRadius;
        _collider.radius = newRadius;
    }

    public void TriggerRadiusEnter()
    {
        OnTriggerRadiusEnter();
    }

    protected abstract void OnTriggerRadiusEnter();

    public void TriggerRadiusExit()
    {
        OnTriggerRadiusExit();
    }

    protected abstract void OnTriggerRadiusExit();
}

public enum MummyRadiusType
{
    Smell,
    Noise,
    Radius
}
