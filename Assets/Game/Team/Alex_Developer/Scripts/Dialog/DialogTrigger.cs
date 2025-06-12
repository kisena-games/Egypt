using App.Scripts.Bootstrap.StateMachine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OverlapSphereTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private List<MonoBehaviour> _scriptsForPause;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Dialog _dialog;

    private void OnEnable()
    {
        _dialog.OnDialogComplete += OnDialogFinished;
    }
    private void OnDisable()
    {
        _dialog.OnDialogComplete -= OnDialogFinished;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        OnStopScripts(true);
        Cursor.lockState = CursorLockMode.None;

    }
    private void OnTriggerExit(Collider other)
    {
        OnStopScripts(false);
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void OnDialogFinished()
    {
        StopAllCoroutines();
        OnStopScripts(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnStopScripts(bool value)
    {
        _dialog.enabled= value;
        _dialogObject.SetActive(value);
        
        foreach (var script in _scriptsForPause)
        {
            script.enabled = !value;
            
        }
        if (value)
        {
            _playerAnimator.enabled = false;
        }
        else _playerAnimator.enabled = true;
    }
}