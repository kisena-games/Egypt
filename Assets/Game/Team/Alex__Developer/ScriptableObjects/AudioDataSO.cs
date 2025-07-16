
using UnityEngine;


[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]

public class AudioDataSO : ScriptableObject
{
    [Header("Step sounds (громкомть не работает)")]
    [Header("Звук шагов по песку")]
    public AudioSelect sandClip;
    [Header("Звук шагов по воде")]
    public AudioSelect waterClip;
    [Header("Звук шагов по камню")]
    public AudioSelect stoneClip;

    [Header("Fx sounds (громкомть работает)")]
    [Header("Звук исчезновения барьера")]
    public AudioSelect barierClip;
    [Header("Звук взятия предмета")]
    public AudioSelect interactableClip;
    [Header("Звук использования предмета")]
    public AudioSelect frescoClip;
    [Header("Говард умирает")]
    public AudioSelect hovardRoarClip;
    [Header("Говард стонет")]
    public AudioSelect hovardMoanClip;
    [Header("Мумия нападает")]
    public AudioSelect mummyAwakeClip;
    [Header("Мумия бьет")]
    public AudioSelect mummyBitClip;

}
[System.Serializable]
public class AudioSelect
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume=1f;
}