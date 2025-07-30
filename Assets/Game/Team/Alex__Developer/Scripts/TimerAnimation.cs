using UnityEngine;
using UnityEngine.UI;

public class TimerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _timerMummy;
    [SerializeField] private Image _imageTimerMummy;
    [SerializeField] private GameObject _timerScarab;
    [SerializeField] private Image _imageTimerScarab;
    [SerializeField] private ParticleSystem _scarabParticle;
    
    private bool _isMummyRunning,_isScarabRunning,_isPlayParticle;



    private void OnEnable()
    {
        PlayerAttacking.OnMummyWait += OnMummyWait;
        PlayerAttacking.OnScarabWait += OnScarabWait;
    }
    private void OnDisable()
    {
        PlayerAttacking.OnMummyWait -= OnMummyWait;
        PlayerAttacking.OnScarabWait -= OnScarabWait;
    }
    private void OnMummyWait()
    {
        _isMummyRunning = true;
        _isScarabRunning = true;
        _isPlayParticle = true;

        // Сброс значений таймера при каждой новой анимации
        _imageTimerMummy.fillAmount = 1f;
        _timerMummy.SetActive(true);

        _imageTimerScarab.fillAmount = 1f;
        _timerScarab.SetActive(true);
    }
    private void OnScarabWait()
    {
        _isScarabRunning = true;
        _isPlayParticle = true;

        _imageTimerScarab.fillAmount = 1f;
        _timerScarab.SetActive(true);
    }

    private void Update()
    {
        if (_isMummyRunning)
        {
            _imageTimerMummy.fillAmount -= Time.deltaTime * 0.2f;
            if (_imageTimerMummy.fillAmount <= 0)
            {
                _isMummyRunning = false;
                _imageTimerMummy.fillAmount = 0f;
                _timerMummy.SetActive(false);
            }
        }

        if (_isScarabRunning)
        {
            _imageTimerScarab.fillAmount -= Time.deltaTime / 15f;
            if (_imageTimerScarab.fillAmount <= 0)
            {
                _isScarabRunning = false;
                _imageTimerScarab.fillAmount = 0f;
                _timerScarab.SetActive(false);
            }
        }

        if (_isPlayParticle)
        {
            _scarabParticle.Play();
            _isPlayParticle = false;
        }
    }

}
