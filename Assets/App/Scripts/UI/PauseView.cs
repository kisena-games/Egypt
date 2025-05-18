using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class PauseView : BaseView
    {
        private bool _isPaused;
        public event Action DebugPanelOpen;
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _developerButton;

        private void Awake()
        {
            if (_resumeButton)
                _resumeButton.onClick.AddListener(Resume);
            if (_developerButton)
                _developerButton.onClick.AddListener(() => DebugPanelOpen?.Invoke());
        }

        public override void Initialize()
        {
            base.Initialize();
            
        }

        public override void Show()
        {
            base.Show();
        }

        public void Pause()
        {
            if (_isPaused)
                return;

            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isPaused = true;
            gameObject.SetActive(true);
        }

        public void Resume()
        {
            if (!_isPaused)
                return;

            Time.timeScale = 1.0f;
            _isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
        
    }
}