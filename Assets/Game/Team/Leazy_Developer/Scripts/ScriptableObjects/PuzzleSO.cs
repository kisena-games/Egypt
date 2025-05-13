using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleSO", menuName = "Scriptable Objects/PuzzleSO")]
public class PuzzleSO : ScriptableObject
{
    public PuzzleEnumType puzzleType;
    public Sprite image;
}
