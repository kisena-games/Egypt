using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerListener : MonoBehaviour
{
    public static PuzzleEnumType interactTrigger { get; private set; }

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
        /*
        if (other.TryGetComponent(out IInteractable interactObject))
        {
            if (_lastObject == null)
            {
                interactObject.Highlight(true);
                _lastObject = interactObject;
            }
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IMummyRadius mummyRadius))
        {
            mummyRadius.TriggerRadiusExit();
        }
        /*
        if (other.TryGetComponent(out IInteractable interactObject))
        {
            if (_lastObject == interactObject)
            {
                _lastObject.Highlight(false);
                _lastObject = null;
            }
        }*/
    }

    private void Update()
    {

        float radius = 2f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        IInteractable nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IInteractable>(out var interactObject))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = interactObject;
                }
                ////////////////////////////////////////////////////////////////////////////////////
                //interactTrigger = hit.GetComponent<PuzzleObject>().//enum value///////////////////
                ////////////////////////////////////////////////////////////////////////////////////
            }
        }

        if (nearest != _lastObject)
        {
            if (_lastObject != null)
                _lastObject.Highlight(false);
            if (nearest != null)
                nearest.Highlight(true);
            _lastObject = nearest;
        }
        if (Input.GetKey(KeyCode.E) && _lastObject != null)
        {
            _lastObject.Interact(_inventory);
        }


        
    }
}
