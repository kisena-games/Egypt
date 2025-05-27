using System;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class SettingsView : BaseView
    {
        public Button BackButton;
        public Button LeftFpsButton;
        public Button RightButtonFps;
        public Toggle VSync;

        public void AddListener(Action onBack)
        {
            BackButton.onClick.AddListener(() => onBack?.Invoke());
        }

        public void RemoveListener()
        {
            BackButton.onClick.RemoveAllListeners();
        }

    }
}