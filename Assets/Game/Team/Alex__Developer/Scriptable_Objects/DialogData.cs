
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]


[System.Serializable]
public class TextAreaWrapper
{
    [TextArea(3, 10)]  // Минимум 3 строки, максимум 10
    public string text;
}
public class DialogData : ScriptableObject
{

    public string publicName { get { return _name; } }
    
    public List<TextAreaWrapper> texts { get { return _texts; } }
    public List<Sprite> images { get { return _images; } }

    
    [SerializeField] private string _name;
    [SerializeField] private List<TextAreaWrapper> _texts = new List<TextAreaWrapper>();

    [SerializeField] private List<Sprite> _images;




}
