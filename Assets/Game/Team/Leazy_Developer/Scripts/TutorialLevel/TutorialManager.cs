using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialInfo> _infos;
    [SerializeField] private float _minTriggerTime = 2f;

    private int triggersNum = 0;

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
        triggersNum++;

        while (!isTriggerDone || _time < _minTriggerTime)
        {
            if (triggersNum > 1)
            {
                break;
            }

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
                    case TutorialType.Sprint:
                        isTriggerDone = Input.GetKeyDown(KeyCode.LeftShift);
                        break;
                    case TutorialType.Interact:
                        isTriggerDone = Input.GetKeyDown(KeyCode.E);
                        break;
                    case TutorialType.Crouch:
                        isTriggerDone = Input.GetKeyDown(KeyCode.LeftControl);
                        break;
                    case TutorialType.Inventory:
                        isTriggerDone = Input.GetKeyDown(KeyCode.E);
                        break;
                }
            }

            _time += Time.deltaTime;

            yield return null;
        }

        info.CanvasPanel.SetActive(false);
        triggersNum--;
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
    Jump,
    Sprint,
    Interact,
    Crouch,
    Inventory
}
