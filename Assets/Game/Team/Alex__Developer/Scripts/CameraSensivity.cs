
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;


public class CameraSensivity : MonoBehaviour
{
    [SerializeField] private CinemachineInputAxisController axisController;
    [SerializeField] private Slider _slider;

    private string sliderKey = "sliderValue";

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(sliderKey, 0.5f);

        _slider.onValueChanged.AddListener(SaveSliderValue);
    }
    void SaveSliderValue(float value)
    {
        PlayerPrefs.SetFloat(sliderKey, value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(SaveSliderValue);
    }
    private void Update()
    {
        foreach (var controller in axisController.Controllers)
        {
            if (controller.Name == "Look Orbit X")
                controller.Input.Gain = _slider.value;
            if (controller.Name == "Look Orbit Y")
                controller.Input.Gain = - _slider.value;

        }
    }
}

