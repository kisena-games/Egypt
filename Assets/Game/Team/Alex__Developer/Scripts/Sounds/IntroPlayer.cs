
using UnityEngine;
using UnityEngine.UI;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] IntroDataSO _introDataSO;
    [SerializeField] Image _image;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audio;
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
            _animator.enabled = false;
            _isNext = true;
            
        }
        SetNext(1);
        SetNext(2);
        SetNext(3);
        SetNext(4);
    }
    private void SetNext(int index)
    {
        if (!_isNext && _mouseCount == index)
        {
            transform.localScale = Vector3.one;
            if (_introDataSO.stages[index].clip != null)
            {
                _audio.clip = _introDataSO.stages[index].clip;
                _audio.Play();
            }
            
            if (_introDataSO.stages[index].sprite != null)
            {
                _image.sprite = _introDataSO.stages[index].sprite;
            }
                
            _isNext = true;
        }
    }

}
