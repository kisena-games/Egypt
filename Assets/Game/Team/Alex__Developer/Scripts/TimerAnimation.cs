using UnityEngine;
using UnityEngine.UI;

public class TimerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _timerMummy;
    [SerializeField] private Image _imageTimerMummy;
    [SerializeField] private GameObject _timerScarab;
    [SerializeField] private Image _imageTimerScarab;
    
    private bool _isMummyRunning,_isScarabRunning;



    private void OnEnable()
    {
        PlayerAttacking.OnMummyWait += OnMummyWait;
    }
    private void OnDisable()
    {
        PlayerAttacking.OnMummyWait -= OnMummyWait;
    }
    private void Update()
    {
        if (_isMummyRunning)
        {
            _timerMummy.SetActive(true);
            _imageTimerMummy.fillAmount-=Time.deltaTime*0.2f;
            
        }
        if (_isScarabRunning)
        {
            _timerScarab.SetActive(true);
            _imageTimerScarab.fillAmount -= Time.deltaTime/15f;
        }
        if (_imageTimerMummy.fillAmount < 0)
        {
            _timerMummy.SetActive(false);
            _isMummyRunning = false;
            _imageTimerMummy.fillAmount = 1f;
        }
        if (_imageTimerScarab.fillAmount < 0)
        {
            _timerScarab.SetActive(false);
            _isScarabRunning = false;
            _imageTimerScarab.fillAmount = 1f;
        }
    }
    private void OnMummyWait()
    {
        _isMummyRunning = true;
        _isScarabRunning = true;
    }
}
