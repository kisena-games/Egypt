using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _player_AudioSource;
    [SerializeField] private AudioSource _vfx_AudioSource;
    [SerializeField] private bool _isSandScene;
    [SerializeField] private string _waterTag;
    [Header("Step sounds")]
    [SerializeField] private AudioClip _sandClip;
    [SerializeField] private AudioClip _waterClip;
    [SerializeField] private AudioClip _stoneClip;
    [Header("Fx sounds")]
    [SerializeField] private AudioClip _barierClip;
    [SerializeField] private AudioClip _interactableClip;
    [SerializeField] private AudioClip _frescoClip;



    private void OnEnable()
    {
        Barrier.OnUnlockBarrier += OnUnlockBarrier;
        Puzzle.OnInteractInventory += OnInteractInventory;
        Fresco.OnInteractFresco += OnInteractFresco;
    }
    private void OnDisable()
    {
        Barrier.OnUnlockBarrier -= OnUnlockBarrier;
        Puzzle.OnInteractInventory -= OnInteractInventory;
    }

    private void Start()
    {
        if (_isSandScene)
        {
            _player_AudioSource.clip = _sandClip;
        }
        else
        {
            _player_AudioSource.clip = _stoneClip;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag) && _isSandScene)
        {
            _player_AudioSource.clip = _waterClip;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag)&&_isSandScene)
        {
            _player_AudioSource.clip = _sandClip;
        }
    }
    private void OnUnlockBarrier()
    {
        _vfx_AudioSource.clip = _barierClip;
        _vfx_AudioSource.Play();
    }
    private void OnInteractInventory()
    {
        _vfx_AudioSource.clip = _interactableClip;
        _vfx_AudioSource.Play();
    }
    private void OnInteractFresco()
    {
        _vfx_AudioSource.clip = _frescoClip;
        _vfx_AudioSource.Play();
    }

}
