using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Не забудь добавить это, если используешь Slider

public class CameraSensivity : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private InputActionReference _moveCursorAction; // Используем reference

    public float mouseKeyboardSensitivity = 1.0f;
    public float gamepadSensitivity = 0.5f;

    void OnEnable()
    {
        // Включаем действия
        _moveCursorAction.action.Enable(); // Доступ к действию через .action
    }

    void OnDisable()
    {
        // Отключаем действия
        _moveCursorAction.action.Disable(); // Доступ к действию через .action
    }

    void Update()
    {
        float currentSensitivity = mouseKeyboardSensitivity; // Здесь это не нужно, если ты просто используешь Slider.value
        Vector2 mouseDelta = _moveCursorAction.action.ReadValue<Vector2>(); // Доступ к значению через .action
        Vector2 scaledMouseDelta = mouseDelta * _slider.value*2;
    }
}

