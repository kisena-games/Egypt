using DG.Tweening;
using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] IntroDataSO _introDataSO;
    [SerializeField] Image _image;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audio;
    [SerializeField] Vector3 _scale;
    [SerializeField] int _nextScene=2;
    private bool _isNext;
    private int _mouseCount;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseCount++;
            _isNext = false;
        }
        if (!_isNext && _mouseCount == 0)
        {
            _audio.clip = _introDataSO.stages[0].clip;
            _audio.Play();
            
            _isNext = true;
            
        }
        SetNext(1);
        SetNext(2);
        SetNext(3);
        SetNext(4);
        if (_mouseCount >= 5)
        {
            SceneManager.LoadScene(_nextScene);
        }
    }
    private void SetNext(int index)
    {
        if (!_isNext && _mouseCount == index)
        {
            

            if (_introDataSO.stages[index].clip != null)
            {
                _audio.clip = _introDataSO.stages[index].clip;
                _audio.Play();
            }
            else
                _audio.Stop();

            if (_introDataSO.stages[index].sprite != null)
            {
                _image.DOFade(0f, 1f).OnComplete(()=> OnSpritesAnimationComplete(index));
            }

            _isNext = true;
        }
    }
    private void OnSpritesAnimationComplete(int index)
    {
        _animator.enabled = false;
        transform.localScale = _scale;
        _image.DOFade(1f, 1f);
        _image.sprite = _introDataSO.stages[index].sprite;
    }

}
