using UnityEngine;

public interface IInteractable
{
    void Highlight(bool isActive);
    void Interact(PlayerInventory inventory);
}
