using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class MenuView : BaseView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _avtorsButton;
        [SerializeField] private Button _exitButton;

        public void AddListener(Action onStart, Action onSettings, Action onAvtors, Action onExit)
        {
            _startButton.onClick.AddListener(() => onStart?.Invoke());
            _settingsButton.onClick.AddListener(() => onSettings?.Invoke());
            _avtorsButton.onClick.AddListener(() => onAvtors?.Invoke());
            _exitButton.onClick.AddListener(() => onExit?.Invoke());
        }

        public void RemoveListener()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _avtorsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

    }
}