
using UnityEngine;


[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]

public class AudioDataSO : ScriptableObject
{
    [Header("Step sounds")]

    public AudioSelect sandClip;
    public AudioSelect waterClip;
    public AudioSelect stoneClip;

    [Header("Fx sounds")]

    public AudioSelect barierClip;
    public AudioSelect interactableClip;
    public AudioSelect frescoClip;
    public AudioSelect hovardRoarClip;
    public AudioSelect hovardMoanClip;
    public AudioSelect mummyAwakeClip;
    public AudioSelect mummyBitClip;

}
[System.Serializable]
public class AudioSelect
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume=1f;
}