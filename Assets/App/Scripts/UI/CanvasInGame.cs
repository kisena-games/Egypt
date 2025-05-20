using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.UI
{
    public class CanvasInGame : BaseView
    {
        [SerializeField] private float _updateDelay = 0.2f;
        [SerializeField] private int _maxFPSDisplayed = 300;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        [FormerlySerializedAs("PauseMenu")] public PauseView pauseView;
        
        private float _elapsedTime = 0.0f;

        private string[] _stringNumbers;

        private void Awake()
        {
            _stringNumbers = new string[_maxFPSDisplayed + 1];
            for (int i = 0; i < _stringNumbers.Length; i++)
            {
                _stringNumbers[i] = i.ToString();
            }
        }

        private void Start()
        {
            _maxFPSDisplayed = Mathf.Clamp(_maxFPSDisplayed, 0, _stringNumbers.Length);
        }

        private void Update()
        {
            _elapsedTime += Time.unscaledDeltaTime;

            if (_elapsedTime >= _updateDelay)
            {
                int currentFPS = (int)(1 / Time.unscaledDeltaTime);
                currentFPS = Mathf.Clamp(currentFPS, 0, _maxFPSDisplayed);

                _textMeshPro.text = _stringNumbers[currentFPS];
                _elapsedTime = 0.0f;
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
                pauseView.Pause();
        }
    }
}
