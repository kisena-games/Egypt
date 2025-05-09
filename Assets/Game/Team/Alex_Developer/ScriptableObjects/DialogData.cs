
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    [SerializeField] private List<string> _texts;
    [SerializeField] private Image _image;

    public List<string> texts { get { return _texts; } }
    public Image image { get { return _image; } }


}
