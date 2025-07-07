using UnityEngine;
using System.Collections.Generic;

public class SyncLightPulse : MonoBehaviour
{
    public List<Light> pointLights;  // Список всех Point Lights
    public float minIntensity = 2f;  // Минимальная яркость
    public float maxIntensity = 10f; // Максимальная яркость
    public float pulseSpeed = 1f;    // Скорость пульсации

    void Update()
    {
        // Рассчитываем текущую интенсивность (синусоида от 0 до 1)
        float pulseValue = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f; 
        // Масштабируем между min и max
        float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, pulseValue);

        // Применяем ко всем лайтам
        foreach (Light light in pointLights)
        {
            light.intensity = currentIntensity;
        }
    }
}