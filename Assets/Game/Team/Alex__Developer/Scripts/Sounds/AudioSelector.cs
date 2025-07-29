using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using EventWwise = AK.Wwise.Event;
using SwithWwise = AK.Wwise.Switch;

public class AudioSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _player_AudioSource;
    [SerializeField] private List<AudioSource> _vfx_AudioSources;
    
    [SerializeField] private bool _isSandScene;
    [SerializeField] private string _waterTag;

    [SerializeField] private AudioDataSO _audioDataSO;

    public EventWwise wwiseOnInteractInventory,
        wwiseOnUnlockBarrier, wwiseOnInteractFresco,
        wwiseOnMoanAudioClip, wwiseOnRoarAudioClip,
        wwiseOnAnubisLook, wwiseOnMummyLook,
        wwiseOnAnubisBit, wwiseOnMummyBit,
        wwiseStepGravel, wwiseStepSand;


    private void OnEnable()
    {
        Barrier.OnUnlockBarrier += OnUnlockBarrier;
        Puzzle.OnInteractInventory += OnInteractInventory;
        Fresco.OnInteractFresco += OnInteractFresco;
        PlayerHealth.OnMoanAudioClip += OnMoanAudioClip;
        PlayerHealth.OnRoarAudioClip += OnRoarAudioClip;
        MummyAttackState.OnAnubisLook += OnAnubisLook;
        MummyAttackState.OnMummyLook += OnMummyLook;
        MummyKillingState.OnAnubisBit += OnAnubisBit;
        MummyKillingState.OnMummyBit += OnMummyBit;
        ActiveState.OnPlayerWalk += OnPlayerWalk;
    }
    private void OnDisable()
    {
        Barrier.OnUnlockBarrier -= OnUnlockBarrier;
        Puzzle.OnInteractInventory -= OnInteractInventory;
        Fresco.OnInteractFresco -= OnInteractFresco;
        PlayerHealth.OnMoanAudioClip -= OnMoanAudioClip;
        PlayerHealth.OnRoarAudioClip -= OnRoarAudioClip;
        MummyAttackState.OnAnubisLook -= OnAnubisLook;
        MummyAttackState.OnMummyLook -= OnMummyLook;
        MummyKillingState.OnAnubisBit -= OnAnubisBit;
        MummyKillingState.OnMummyBit -= OnMummyBit;
        ActiveState.OnPlayerWalk -= OnPlayerWalk;
    }

    private void Start()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag) && _isSandScene)
        {
 
            //_player_AudioSource.clip = _audioDataSO.waterClip.clip;
            //_player_AudioSource.volume = _audioDataSO.waterClip.volume;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_waterTag)&&_isSandScene)
        {
      
            //_player_AudioSource.clip = _audioDataSO.sandClip.clip;
            //_player_AudioSource.volume = _audioDataSO.sandClip.volume;
        }
    }
    private void OnUnlockBarrier()
    {
        //FXPlay(_vfx_AudioSources[0], _audioDataSO.barierClip.clip, _audioDataSO.barierClip.volume);
        
    }
    private void OnInteractInventory()
    {
        //FXPlay(_vfx_AudioSources[0], _audioDataSO.interactableClip.clip, _audioDataSO.interactableClip.volume);
        
    }
    private void OnInteractFresco()
    {
        //FXPlay(_vfx_AudioSources[0], _audioDataSO.frescoClip.clip, _audioDataSO.frescoClip.volume);
    }
    private void OnMoanAudioClip()
    {
        //FXPlay(_vfx_AudioSources[1], _audioDataSO.hovardMoanClip.clip, _audioDataSO.hovardMoanClip.volume);
    }
    private void OnMummyBit()
    {
       // FXPlay(_vfx_AudioSources[2], _audioDataSO.mummyBitClip.clip, _audioDataSO.mummyBitClip.volume);
    }
    private void OnAnubisBit()
    {
       // FXPlay(_vfx_AudioSources[4], _audioDataSO.anubisBitClip.clip, _audioDataSO.anubisBitClip.volume);
    }
   
    private void OnRoarAudioClip()
    {
        //FXPlay(_vfx_AudioSources[3], _audioDataSO.hovardRoarClip.clip, _audioDataSO.hovardRoarClip.volume);
    }
    private void OnAnubisLook()
    {
       // FXPlay(_vfx_AudioSources[4], _audioDataSO.anubisAwakeClip.clip, _audioDataSO.anubisAwakeClip.volume);
    }
    private void OnMummyLook()
    {
        wwiseOnInteractInventory.Post(gameObject);
        //FXPlay(_vfx_AudioSources[2], _audioDataSO.mummyAwakeClip.clip, _audioDataSO.mummyAwakeClip.volume);
    }
    private void OnPlayerWalk()
    {
        if (_isSandScene)
        {
            wwiseStepSand.Post(gameObject);
            //_player_AudioSource.clip = _audioDataSO.sandClip.clip;
            // _player_AudioSource.volume = _audioDataSO.sandClip.volume;

        }
        else
        {
            wwiseStepGravel.Post(gameObject);
            //_player_AudioSource.clip = _audioDataSO.stoneClip.clip;
            //_player_AudioSource.volume = _audioDataSO.stoneClip.volume;
        }
    }
    private void OnPlayerStealth()
    {

    }
    private void FXPlay(AudioSource audioSource, AudioClip clip,float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }


}
