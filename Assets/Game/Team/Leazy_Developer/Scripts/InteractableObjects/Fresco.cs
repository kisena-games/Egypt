using UnityEngine;
using UnityEngine.UI;

public class Fresco : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleEnumType _puzzleToInteract;
    [SerializeField] private Light _light;

    public bool IsActivated { get; private set; }

    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void Highlight(bool isActive)
    {
        if (_outline != null && !IsActivated)
        {
            _outline.enabled = isActive;
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (!IsActivated)
        {
            PuzzleSO inventoryItem = inventory.GetActiveItem();

            if (inventoryItem != null && inventoryItem.puzzleType == _puzzleToInteract)
            {
                inventory.RemoveActiveItem();
                IsActivated = true;
                _outline.enabled = false;
                _light.enabled = true;
            }
        }
    }
}
