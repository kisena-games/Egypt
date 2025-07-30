
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static int healthCount = 3;
    public static Action OnRoarAudioClip;
    public static Action OnMoanAudioClip;


    [SerializeField] private int _sceneIndex;
    [SerializeField] private Image _imageDark;
    [SerializeField] private Image _blood;
    [SerializeField] private Image _bloodBack;

    private int _preCount;
    private float _emisson;
    private void Start()
    {
        healthCount = 3;
        _preCount= healthCount;
        StartCoroutine(waitSceneInit());
    }
    private IEnumerator waitSceneInit()
    {
        yield return new WaitForSeconds(0.3f);
        _imageDark.DOColor(colorBlack(0), 1f);
    }
    
    private Color color(float a)
    {
        return new Color(1,1,1,a);
    }
    
    private Color colorRed(float a)
    {
        return new Color(1, 0, 0, a);
    }
    private Color colorBlack(float a)
    {
        return new Color(0, 0, 0, a);
    }
    private void Update()
    {
        if (_preCount> healthCount&&!(healthCount<=0))
        {
            OnMoanAudioClip?.Invoke();
            _emisson += 0.4f;
            _blood.DOColor(color(_emisson), 0.5f);

            _bloodBack.DOColor(colorRed(_emisson), 0.5f)
                .OnComplete(() => {
                    _bloodBack.DOColor(colorRed(0), 1f);
                });
            _preCount = healthCount;
           
        }
        

        if (healthCount <= 0)
        {
            OnRoarAudioClip?.Invoke();
            Debug.Log("Audio");
            StopAllCoroutines();
            _imageDark.DOColor(colorBlack(1f), 1f)
                .OnComplete(()=>SceneManager.LoadScene(_sceneIndex));
            _blood.DOColor(colorBlack(1f), 2f);
            healthCount = 3;
        }
        
    }
}
