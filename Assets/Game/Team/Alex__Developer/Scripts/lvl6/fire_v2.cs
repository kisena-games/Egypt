using UnityEngine;

public class fire_v2 : MonoBehaviour
{
    public Light pointLight;
    public float minIntensity = 2f;
    public float maxIntensity = 5f;
    public float changeInterval = 0.1f;  // Частота обновления цели
    public float smoothTime = 0.3f;     // Время плавного перехода

    private float timer;
    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        if (pointLight == null)
            pointLight = GetComponent<Light>();
        
        timer = changeInterval;
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        currentIntensity = pointLight.intensity;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            // Выбираем новую случайную интенсивность
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            timer = changeInterval;
        }

        // Плавно меняем интенсивность
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime / smoothTime);
        pointLight.intensity = currentIntensity;
    }
}