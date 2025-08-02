using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autors : MonoBehaviour
{
    [SerializeField] private List<Panel> _panel;
    private void OnEnable()
    {
        foreach (var panel in _panel)
        {
            var capturedPanel = panel;
            panel.button.onClick.AddListener(() => OpenWebsite(capturedPanel));
        }
    }
    private void OnDisable()
    {
        foreach (var panel in _panel)
        {
            var capturedPanel = panel;
            panel.button.onClick.RemoveListener(() => OpenWebsite(capturedPanel));
        }
    }
    public void OpenWebsite(Panel panel)
    {
        Application.OpenURL(panel.url);
    }
}
[System.Serializable]
public class Panel
{
    public Button button;
    public string url;
}