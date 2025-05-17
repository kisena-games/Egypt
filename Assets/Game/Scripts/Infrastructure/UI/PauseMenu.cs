using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Infrastructure.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private bool _isPaused;
        [SerializeField] private Button _resumeButton;

        private void Awake()
        {
            if (_resumeButton)
                _resumeButton.onClick.AddListener(Resume);
        }

        public void Pause()
        {
            if (_isPaused)
                return;

            Time.timeScale = 0.0f;
            _isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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