using UnityEngine;
using UnityEngine.UI;

public class FrescoObject : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleEnumType _puzzleToInteract;
    [SerializeField] private RectTransform _selector;
    private Outline _outline;

    public void Highlight(bool isActive)
    {
        _outline.enabled = isActive ? true : false;
    }

    public void Interact(PlayerInventory inventory)
    {
        
        if (PuzzleObject.interactID==0&& _puzzleToInteract==PlayerListener.interactTrigger)
        {
            transform.position = Camera.main.ScreenToWorldPoint(_selector.position);
        }
    }
}
