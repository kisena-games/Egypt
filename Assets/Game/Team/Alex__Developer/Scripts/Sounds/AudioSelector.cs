using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _player_AudioSource;
    [SerializeField] private AudioSource _vfx_AudioSource;
    [SerializeField] private AudioClip _sandAudioClip;
    [SerializeField] private AudioClip _waterAudioClip;
    [SerializeField] private AudioClip _stoneAudioClip;
    [SerializeField] private bool _isSandScene;
    [SerializeField] private string _waterTag;

    private void OnEnable()
    {
        Barrier.OnUnlockBarier += OnUnlockBarrier;
    }
    private void OnDisable()
    {
        Barrier.OnUnlockBarier -= OnUnlockBarrier;
    }

    private void Start()
    {
        if (_isSandScene)
        {
            _player_AudioSource.clip = _sandAudioClip;
        }
        else
        {
            _player_AudioSource.clip = _stoneAudioClip;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag) && _isSandScene)
        {
            _player_AudioSource.clip = _waterAudioClip;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag)&&_isSandScene)
        {
            _player_AudioSource.clip = _sandAudioClip;
        }
    }
    private void OnUnlockBarrier()
    {
        //soud barier
    }

}
