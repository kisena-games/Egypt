
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{

    public string publicName { get { return _name; } }
    public List<string> texts { get { return _texts; } }
    public List<Sprite> images { get { return _images; } }

    
    [SerializeField] private string _name;
    [SerializeField] private List<string> _texts;
    [SerializeField] private List<Sprite> _images;
}
