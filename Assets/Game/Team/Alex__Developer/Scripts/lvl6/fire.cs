using UnityEngine;

public class Fire : MonoBehaviour
{
    public Light pointLight;  // Ссылка на Point Light
    public float minIntensity = 2f;  // Минимальная интенсивность
    public float maxIntensity = 5f;  // Максимальная интенсивность
    public float changeInterval = 0.5f;  // Как часто меняется интенсивность (в секундах)

    private float timer;

    void Start()
    {
        if (pointLight == null)  // Если свет не назначен вручную, берём его с этого же объекта
        {
            pointLight = GetComponent<Light>();
        }
        timer = changeInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            // Меняем интенсивность на случайное значение между minIntensity и maxIntensity
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
            
            // Сбрасываем таймер
            timer = changeInterval;
        }
    }
}