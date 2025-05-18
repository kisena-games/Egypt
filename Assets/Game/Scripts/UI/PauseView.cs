using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class PauseView : BaseView
    {
        private bool _isPaused;
        [SerializeField] private Button _resumeButton;

        private void Awake()
        {
            if (_resumeButton)
                _resumeButton.onClick.AddListener(Resume);
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