
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "IntroData", menuName = "Scriptable Objects/IntroData")]

public class IntroDataSO : ScriptableObject
{
    [Header("Последовательность окон")]
    public List<IntroSelect> stages;

}
[System.Serializable]
public class IntroSelect
{
    public AudioClip clip;
    public Sprite sprite;
    [Range(0f, 1f)]
    public float volume = 1f;

}