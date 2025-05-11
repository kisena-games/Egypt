using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialInfo> _infos;
    [SerializeField] private float _minTriggerTime = 2f;

    //[SerializeField] private GameObject _wasd_panel;
    //[SerializeField] private GameObject _sprint_panel;
    //[SerializeField] private GameObject _jump_panel;

    public void Trigger(TutorialType type)
    {
        foreach (TutorialInfo info in _infos)
        {
            if (info.Type == type)
            {
                StartCoroutine(StartTutorial(info));
                break;
            }
        }
    }

    IEnumerator StartTutorial(TutorialInfo info)
    {
        info.CanvasPanel.SetActive(true);
        bool isTriggerDone = false;
        float _time = 0f;

        while (!isTriggerDone || _time < _minTriggerTime)
        {
            if (!isTriggerDone)
            {
                switch (info.Type)
                {
                    case TutorialType.Move:
                        isTriggerDone = InputManager.Instance.IsMoving;
                        break;
                    case TutorialType.Jump:
                        isTriggerDone = Input.GetKeyDown(KeyCode.Space);
                        break;
                }
            }

            _time += Time.deltaTime;

            yield return null;
        }

        info.CanvasPanel.SetActive(false);
    }
}

[Serializable]
public class TutorialInfo
{
    [field: SerializeField] public TutorialType Type { get; private set; }
    [field: SerializeField] public GameObject CanvasPanel { get; private set; }
}

public enum TutorialType
{
    Move,
    Sprint,
    Jump
}
