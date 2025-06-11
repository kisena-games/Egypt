using App.Scripts.Bootstrap.StateMachine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OverlapSphereTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private List<MonoBehaviour> _scriptsForPause;
    [SerializeField] private Dialog _dialog;

    private void OnEnable()
    {
        _dialog.OnDialogComplete += OnDialogFinished;
    }
    private void OnDisable()
    {
        _dialog.OnDialogComplete -= OnDialogFinished;
    }
    private void OnDialogFinished()
    {
        Debug.Log("F");
        OnStopScripts(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        OnStopScripts(true);

    }
    private void OnTriggerExit(Collider other)
    {
        OnStopScripts(false);

    }
    private void OnStopScripts(bool value)
    {
        _dialogObject.SetActive(value);
        foreach (var script in _scriptsForPause)
        {
            script.enabled = !value;
        }
    }
}