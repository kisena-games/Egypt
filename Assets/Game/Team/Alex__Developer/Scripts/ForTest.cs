using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForTest : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause,_inputManager,_inventory;

    private string inputBuffer = "";
    private float inputTimer = 0f;
    private float inputTimeout = 2f; // Таймаут на ввод команды (сек)

    private void OpenMenu()
    {
        _canvasPause.SetActive(true);
        _inputManager.SetActive(false);
        _inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        /*
        // Обработка быстрого переключения цифрами 0-7
        for (int i = 0; i <= 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                int sceneIndex = i * 3;
                SceneManager.LoadScene(sceneIndex);
                // Очистим буфер — для надёжности
                inputBuffer = "";
                inputTimer = 0f;
                return; // Чтобы не обрабатывать ввод дальше в этом кадре
            }
        }
        */

        // Обработка Escape (как у тебя)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_canvasPause.activeSelf)
                _inventory.SetActive(true);
            else
                OpenMenu();
        }

        // Проверка ввода текста для чит-команд
        foreach (char c in Input.inputString)
        {
            inputBuffer += c;
            inputTimer = 0f; // Сброс таймера при вводе новой буквы
        }

        if (inputBuffer.Length > 0)
        {
            inputTimer += Time.deltaTime;

            if (inputTimer > inputTimeout)
            {
                // Таймаут, сбрасываем буфер
                inputBuffer = "";
                inputTimer = 0f;
            }
            else
            {
                CheckInputBuffer();
            }
        }
    }

    private void CheckInputBuffer()
    {
        // Пробегаем по командам fara1 - fara7
        for (int i = 1; i <= 7; i++)
        {
            string command = "fara" + i;
            if (inputBuffer.EndsWith(command))
            {
                Debug.Log($"Cheat detected: {command}, loading scene {i}");
                SceneManager.LoadScene(i*3);
                inputBuffer = "";
                inputTimer = 0f;
                break;
            }
        }
    }

}
