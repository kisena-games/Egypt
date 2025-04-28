using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    private IInteractable _lastObject;
    private PlayerInventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IMummyRadius mummyRadius))
        {
            mummyRadius.TriggerRadiusEnter();
        }

        if (other.TryGetComponent(out IInteractable interactObject))
        {
            if (_lastObject == null)
            {
                interactObject.Highlight(true);
                _lastObject = interactObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IMummyRadius mummyRadius))
        {
            mummyRadius.TriggerRadiusExit();
        }

        if (other.TryGetComponent(out IInteractable interactObject))
        {
            if (_lastObject == interactObject)
            {
                _lastObject.Highlight(false);
                _lastObject = null;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && _lastObject != null)
        {
            _lastObject.Interact(_inventory);
        }
    }
}
