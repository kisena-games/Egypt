using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private Toggle _toggle;

    [SerializeField] private SaveToJson _saveToJson;

    private int _currentFPS = -1;

    private List<int> _keysFPS;
    private Dictionary<int, string> _keyValueFPS = new Dictionary<int, string>
    {
        { 30, "30" },
        { 60, "60" },
        { 90, "90" },
        { 120, "120" },
        { -1, "No limit" },
    };

    private void Awake()
    {
        _keysFPS = new List<int>(_keyValueFPS.Keys);
        Cursor.lockState= CursorLockMode.None;
    }

    private void Start()
    {
        _currentFPS = Application.targetFrameRate;
        if (QualitySettings.vSyncCount == 1 && !_toggle.isOn)
        {
            _toggle.isOn = true;
        }
        else if (QualitySettings.vSyncCount == 0 && _toggle.isOn)
        {
            _toggle.isOn = false;
        }

        UpdateFPS();
    }

    public void LeftArrow()
    {
        ChangeFPS(-1);
    }

    public void RightArrow()
    {
        ChangeFPS(1);
    }

    public void VSyncOnOff()
    {
        QualitySettings.vSyncCount = _toggle.isOn ? 1 : 0;
        Debug.Log(QualitySettings.vSyncCount);
    }

    private void ChangeFPS(int direction)
    {
        int index = _keysFPS.IndexOf(_currentFPS);

        index = Mathf.Clamp(index + direction, 0, _keysFPS.Count - 1);
        _currentFPS = _keysFPS[index];

        UpdateFPS();
    }

    private void UpdateFPS()
    {
        if (_currentFPS == -1)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = _currentFPS;
        }

        _fpsText.text = _keyValueFPS[_currentFPS];
    }

    public void LoadTestScene()
    {
        if (_saveToJson.GetLevel() > 0)
        {
            SceneManager.LoadScene(_saveToJson.GetLevel());
        }
        else
            SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
