using UnityEngine;

public class PuzzleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleEnumType _puzzleType;

    public void Highlight(bool isActive)
    {
        // вкл,выкл подсветки
    }

    public void Interact(PlayerInventory inventory)
    {
        // логика подбора предмета
    }
}

public enum PuzzleEnumType
{
    BlueSphere,
    RedSphere
}
