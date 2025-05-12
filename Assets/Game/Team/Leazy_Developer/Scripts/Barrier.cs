using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private List<Fresco> _frescos;

    private void Start()
    {
        StartCoroutine(TryUnlockBarrier());
    }

    private IEnumerator TryUnlockBarrier()
    {
        bool isCanUnlock = false;

        while (!isCanUnlock)
        {
            Debug.Log(isCanUnlock);

            isCanUnlock = true;

            foreach (var fresco in _frescos)
            {
                if (!fresco.IsActivated)
                {
                    isCanUnlock = false;
                    break;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
