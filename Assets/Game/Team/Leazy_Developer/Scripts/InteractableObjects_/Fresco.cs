using System;
using UnityEngine;
using UnityEngine.UI;

public class Fresco : MonoBehaviour, IInteractable
{
    public static Action OnInteractFresco;
    [SerializeField] private PuzzleEnumType _puzzleToInteract;
    [SerializeField] private Light _light;
    [SerializeField] private NewOutline _outline;
    public bool IsActivated { get; private set; }

    

    private void Awake()
    {
        //_outline = GetComponent<Outline>();
    }

    public void Highlight(bool isActive)
    {
        if (_outline != null && !IsActivated)
        {
            _outline.Enabled = isActive;
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
                _outline.Enabled = false;
                _light.enabled = true;
                OnInteractFresco?.Invoke();
            }
        }
    }
}
