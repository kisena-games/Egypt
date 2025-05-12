using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerListener : MonoBehaviour
{
    [Header("Interact Parameters")]
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private float _updateInteractTime = 0.5f;

    public IInteractable LastObject { get; private set; }

    private PlayerInventory _inventory;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactDistance);
    }

    private void Awake()
    {
        _inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        StartCoroutine(ListenInteractableObjects());
    }

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

    private void Update()
    {
        CheckInteract();
    }

    private void CheckInteract()
    {
        if (Input.GetKeyDown(KeyCode.E) && LastObject != null)
        {
            LastObject.Interact(_inventory);
        }
    }

    private IInteractable GetClosestObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactDistance);

        IInteractable closestObject = null;
        float minObjectDistance = _interactDistance + 1.0f;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable obj))
            {
                float objDistance = Vector3.Distance(transform.position, collider.transform.position);

                if (objDistance < minObjectDistance)
                {
                    minObjectDistance = objDistance;
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }

    private void UpdateClosestObject()
    {
        IInteractable newFoundObject = GetClosestObject();
        if (newFoundObject != null)
        {
            if (newFoundObject != LastObject)
            {
                if (LastObject != null)
                {
                    // убрать эффект взаимодействия со старого LastObject-а
                    LastObject.Highlight(false);
                }

                // добавить эффект взаимодействия на новый LastObject
                LastObject = newFoundObject;
                LastObject.Highlight(true);
            }
        }
        else
        {
            if (LastObject != null)
            {
                // убрать эффект взаимодействия со старого LastObject-а
                LastObject.Highlight(false);
                LastObject = null;
            }
        }
    }

    private IEnumerator ListenInteractableObjects()
    {
        while (true)
        {
            UpdateClosestObject();

            yield return new WaitForSeconds(_updateInteractTime);
        }
    }
}
