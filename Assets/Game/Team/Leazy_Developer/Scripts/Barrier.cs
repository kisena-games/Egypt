using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private List<Fresco> _frescosToActivateBarrier;
    [SerializeField] private List<BarrierConfiguration> _resultConfigurations;

    private void Start()
    {
        StartCoroutine(UpdateBarrier());
    }

    private void UnlockBarrier()
    {
        foreach (var config in _resultConfigurations)
        {
            config.resultObject.SetActive(config.isNeedActive);
        }

        Destroy(gameObject);
    }

    private IEnumerator UpdateBarrier()
    {
        bool isCanUnlock = false;

        while (!isCanUnlock)
        {
            Debug.Log(isCanUnlock);

            isCanUnlock = true;

            foreach (var fresco in _frescosToActivateBarrier)
            {
                if (!fresco.IsActivated)
                {
                    isCanUnlock = false;
                    break;
                }
            }

            yield return new WaitForSeconds(0.4f);
        }

        UnlockBarrier();
    }
}

[Serializable]
public class BarrierConfiguration
{
    public bool isNeedActive;
    public GameObject resultObject;
}
