using UnityEngine;
using EventWwise = AK.Wwise.Event;
using System.Collections;

public class BackAudio : MonoBehaviour
{
    [SerializeField] private EventWwise _eventAudio;

    private void Awake()
    {
        _eventAudio.Post(gameObject);
    }

}
