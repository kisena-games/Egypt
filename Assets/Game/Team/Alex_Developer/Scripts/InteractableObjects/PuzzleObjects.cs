using UnityEngine;
using UnityEngine.Assertions.Must;

public class PuzzleObject : MonoBehaviour, IInteractable
{
    public static int interactID { get; private set; }

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
        interactID = Inventary.indexContainer[Inventary.currentIndex];
        if (interactID == 0)
        {
            interactID = Inventary.currentIndex;
            gameObject.SetActive(false);
        }

    }
}

public enum PuzzleEnumType
{
    None,
    BlueCube,
    GreenSphere,
    RedSphere,
    PurplePyramid
}
