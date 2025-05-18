using UnityEngine;
using UnityEngine.Assertions.Must;

public class Puzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleSO _puzzleSO;

    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void Highlight(bool isActive)
    {
        if (_outline != null)
        {
            _outline.enabled = isActive;
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (inventory.Add(_puzzleSO))
        {
            Destroy(gameObject);
        }
    }
}
