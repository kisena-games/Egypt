using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    [SerializeField] private TutorialManager _tutorialManager;    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TutorialTrigger trigger))
        {
            _tutorialManager.Trigger(trigger.Type);
            trigger.gameObject.SetActive(false);
        }
    }
}
