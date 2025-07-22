using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Добавляем пространство имён DoTween

public class Barrier : MonoBehaviour
{
    [SerializeField] private List<Fresco> _frescosToActivateBarrier;
    [SerializeField] private List<ParticleSystem> _particleSystems;
    [SerializeField] private List<BarrierConfiguration> _resultConfigurations;
    [SerializeField] private List<BarrierConfigurationComponent> _resultConfigurationsComponent;

    public bool isCanUnlock;
    public static Action OnUnlockBarrier;

    

    private void Start()
    {
        StartCoroutine(UpdateBarrier());
    }

    private void UnlockBarrier()
    {
        // Активируем/деактивируем объекты конфигурации
        

        // Плавно уменьшаем прозрачность частиц
        foreach (var ps in _particleSystems)
        {
            var main = ps.main;
            DOTween.To(() => main.startColor.color.a,
                      alpha => {
                          var color = main.startColor.color;
                          color.a = alpha;
                          main.startColor = color;
                      },
                      0f, 3f); 

        }
        foreach (var config in _resultConfigurations)
        {
            config.resultObject.SetActive(config.isNeedActive);
        }
        foreach (var config in _resultConfigurationsComponent)
        {
            config.component.GetComponent<Collider>().enabled=config.isNeedActive;
        }
        //DOVirtual.DelayedCall(1f, () => Destroy(gameObject));
    }

    private IEnumerator UpdateBarrier()
    {
        isCanUnlock = false;

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

            yield return new WaitForSeconds(0.1f);
        }
        OnUnlockBarrier?.Invoke();
        UnlockBarrier();
    }
}

[Serializable]
public class BarrierConfiguration
{
    public bool isNeedActive;
    public GameObject resultObject;
}
[Serializable]
public class BarrierConfigurationComponent
{
    public bool isNeedActive;
    public Collider component;
}
