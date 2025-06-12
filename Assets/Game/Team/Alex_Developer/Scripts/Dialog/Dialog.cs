using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using ModestTree;
using System.Collections;
using DG.Tweening;
using System;



public class Dialog : MonoBehaviour
{
    public Action OnDialogComplete;

    [SerializeField] private KeyCode _skipKey;
    [SerializeField] private Button _skipButton;
    [SerializeField] private List<DialogData> _dialogSequence;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private TextMeshProUGUI _speakerName;
    [SerializeField] private Image _speakerImage;
    [SerializeField] private string _playerName = "Говард";
    [SerializeField] private float _clickCooldown = 0.01f;
    [SerializeField] private float _fadeDuration = 0.2f; // Duration for fade in/out effects

    private Dictionary<string, int> characterTextIndices = new Dictionary<string, int>();
    private Sequence currentSequence;

    private bool isDialogActive = true;
    private bool _isSkip;

    private float lastClickTime;
    

    private void OnEnable()
    {
        _skipButton.interactable = true;
        
        
        // Reset any necessary dialog state here, for example:
        characterTextIndices.Clear(); // Assuming characterTextIndices needs to be reset
        isDialogActive = true;
        _isSkip = false;
        _dialogText.DOFade(1, 0);
        _speakerName.DOFade(1, 0); 
        _speakerImage.DOFade(1, 0); 

        // Start the ShowText coroutine
        StartCoroutine(ShowText());
    }
    
    private void OnDisable()
    {
        
        StopCoroutine(ShowText());
        currentSequence?.Kill();
    }
    private void OnSkipButtonPressed()
    {
        _isSkip = true;
    }
    private IEnumerator ShowText()
    {
        // Initialize character indices
        foreach (var dialog in _dialogSequence)
        {
            if (dialog == null || dialog.texts == null || dialog.texts.Count == 0)
            {
                Debug.LogError($"Invalid dialog data (null or empty texts). Skipping...");
                continue;
            }

            if (!characterTextIndices.ContainsKey(dialog.publicName))
            {
                characterTextIndices[dialog.publicName] = 0;
            }
        }

        while (isDialogActive)
        {
            _skipButton.onClick.AddListener(OnSkipButtonPressed);
            bool anyTextRemaining = false;

            foreach (var dialog in _dialogSequence)
            {
                if (dialog == null || dialog.texts == null ||
                    characterTextIndices[dialog.publicName] >= dialog.texts.Count)
                {
                    continue;
                }

                anyTextRemaining = true;
                int currentIndex = characterTextIndices[dialog.publicName];

                currentSequence = DOTween.Sequence();
                currentSequence.Append(_dialogText.DOFade(0, _fadeDuration / 2));
                currentSequence.Join(_speakerName.DOFade(0, _fadeDuration / 2));
                currentSequence.Join(_speakerImage.DOFade(0, _fadeDuration / 2));

                currentSequence.AppendCallback(() =>
                {
                    _dialogText.text = dialog.texts[currentIndex].text;
                    _speakerName.text = dialog.publicName;

                    if (dialog.images != null && currentIndex < dialog.images.Count && dialog.images[currentIndex] != null)
                    {
                        _speakerImage.sprite = dialog.images[currentIndex];
                        _speakerImage.enabled = true;
                    }
                    else
                    {
                        _speakerImage.enabled = false;
                    }
                });

                currentSequence.Append(_dialogText.DOFade(1, _fadeDuration));
                currentSequence.Join(_speakerName.DOFade(1, _fadeDuration));
                if (_speakerImage.enabled)
                {
                    currentSequence.Join(_speakerImage.DOFade(1, _fadeDuration));
                }

                yield return currentSequence.WaitForCompletion();

                // Вместо просто ожидания клика, добавим возможность пропуска диалога по Escape
                bool clickedOrSkipped = false;
                while (!clickedOrSkipped)
                {
                    if (Input.GetMouseButtonDown(0) && Time.time - lastClickTime > _clickCooldown)
                    {
                        lastClickTime = Time.time;
                        clickedOrSkipped = true;
                    }
                    else if (Input.GetKeyDown(_skipKey) || _isSkip)
                    {
                        isDialogActive = false;
                        clickedOrSkipped = true;
                        OnDialogComplete?.Invoke();
                        gameObject.SetActive(false);
                        StopCoroutine(ShowText());
                        break;
                    }
                    yield return null;
                }

                if (!isDialogActive)
                    break;

                characterTextIndices[dialog.publicName]++;
            }

            // Если диалог завершён, затем затухаем и вызовем событие
            if (!anyTextRemaining || !isDialogActive)
            {
                currentSequence = DOTween.Sequence();
                currentSequence.Append(_dialogText.DOFade(0, _fadeDuration));
                currentSequence.Join(_speakerName.DOFade(0, _fadeDuration));
                currentSequence.Join(_speakerImage.DOFade(0, _fadeDuration));
                yield return currentSequence.WaitForCompletion();

                isDialogActive = false;
                _isSkip = false;
                OnDialogComplete?.Invoke(); // Вызов события завершения диалога

                yield break;
            }
        }
    }
}
