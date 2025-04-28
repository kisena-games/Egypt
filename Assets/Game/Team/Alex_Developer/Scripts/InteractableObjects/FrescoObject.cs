using UnityEngine;

public class FrescoObject : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleEnumType _puzzleToInteract;

    private bool _isEpmty = true;

    public void Highlight(bool isActive)
    {

    }

    public void Interact(PlayerInventory inventory)
    {
        // логика проверки активного предмета в инвентаре на нужны к жтому объекту
        //
    }
}
