using UnityEngine;
using UnityEngine.Assertions.Must;

public class PuzzleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleEnumType _puzzleType;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void Highlight(bool isActive)
    {
        _outline.enabled = isActive ? true: false;
    }

    public void Interact(PlayerInventory inventory)
    {
        // логика подбора предмета
    }
}

public enum PuzzleEnumType
{
    RedCube,
    GreenSphere,
    BlueCapsule//
}
