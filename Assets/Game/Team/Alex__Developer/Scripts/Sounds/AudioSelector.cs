using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using EventWwise = AK.Wwise.Event;

public class AudioSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _player_AudioSource;
    [SerializeField] private List<AudioSource> _vfx_AudioSources;
    
    [SerializeField] private bool _isSandScene;
    [SerializeField] private string _waterTag;

    [SerializeField] private AudioDataSO _audioDataSO;

    public EventWwise wwiseOnInteractInventory;

    private void OnEnable()
    {
        Barrier.OnUnlockBarrier += OnUnlockBarrier;
        Puzzle.OnInteractInventory += OnInteractInventory;
        Fresco.OnInteractFresco += OnInteractFresco;
        PlayerHealth.OnMoanAudioClip += OnMoanAudioClip;
        PlayerHealth.OnRoarAudioClip += OnRoarAudioClip;
        MummyAttackState.OnLook += OnLook;
        MummyKillingState.OnBit += OnBit;
    }
    private void OnDisable()
    {
        Barrier.OnUnlockBarrier -= OnUnlockBarrier;
        Puzzle.OnInteractInventory -= OnInteractInventory;
        Fresco.OnInteractFresco -= OnInteractFresco;
        PlayerHealth.OnMoanAudioClip -= OnMoanAudioClip;
        PlayerHealth.OnRoarAudioClip -= OnRoarAudioClip;
        MummyAttackState.OnLook -= OnLook;
        MummyKillingState.OnBit -= OnBit;
    }

    private void Start()
    {
        if (_isSandScene)
        {
            _player_AudioSource.clip = _audioDataSO.sandClip.clip;
            _player_AudioSource.volume = _audioDataSO.sandClip.volume;
        }
        else
        {
            _player_AudioSource.clip = _audioDataSO.stoneClip.clip;
            _player_AudioSource.volume = _audioDataSO.stoneClip.volume;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag) && _isSandScene)
        {
            _player_AudioSource.clip = _audioDataSO.waterClip.clip;
            _player_AudioSource.volume = _audioDataSO.waterClip.volume;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag)&&_isSandScene)
        {
            _player_AudioSource.clip = _audioDataSO.sandClip.clip;
            _player_AudioSource.volume = _audioDataSO.sandClip.volume;
        }
    }
    private void OnUnlockBarrier()
    {
        FXPlay(_vfx_AudioSources[0], _audioDataSO.barierClip.clip, _audioDataSO.barierClip.volume);
        
    }
    private void OnInteractInventory()
    {
       // FXPlay(_vfx_AudioSources[0], _audioDataSO.interactableClip.clip, _audioDataSO.interactableClip.volume);
        wwiseOnInteractInventory.Post(gameObject);
    }
    private void OnInteractFresco()
    {
        FXPlay(_vfx_AudioSources[0], _audioDataSO.frescoClip.clip, _audioDataSO.frescoClip.volume);
    }
    private void OnMoanAudioClip()
    {
        FXPlay(_vfx_AudioSources[1], _audioDataSO.hovardMoanClip.clip, _audioDataSO.hovardMoanClip.volume);
    }
    private void OnBit()
    {
        FXPlay(_vfx_AudioSources[2], _audioDataSO.mummyBitClip.clip, _audioDataSO.mummyBitClip.volume);
    }
    private void OnRoarAudioClip()
    {
        FXPlay(_vfx_AudioSources[3], _audioDataSO.hovardRoarClip.clip, _audioDataSO.hovardRoarClip.volume);
    }
    private void OnLook()
    {
        FXPlay(_vfx_AudioSources[0], _audioDataSO.mummyAwakeClip.clip, _audioDataSO.mummyAwakeClip.volume);
    }
    private void FXPlay(AudioSource audioSource, AudioClip clip,float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }


}
