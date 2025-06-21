using UnityEngine;
using UnityEngine.Assertions.Must;

public class Puzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleSO _puzzleSO;

    [SerializeField] private NewOutline _outline;

    private void Awake()
    {
        //_outline = GetComponent<Outline>();
    }

    public void Highlight(bool isActive)
    {
        if (_outline != null)
        {
            _outline.Enabled = isActive;
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
