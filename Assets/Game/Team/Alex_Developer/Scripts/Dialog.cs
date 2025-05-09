using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogData[] _dialogDataObjects; // два объекта ScriptableObject
    [SerializeField] private int _currentDialogIndex = 0; // индекс текущего объекта
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Image _imageUI;

    private int _currentTextIndex = 0;
    private Coroutine _typingCoroutine;
    private bool _isTyping = false;

    private float _typingSpeed = 0.05f;
    private float _fastTypingSpeed => _typingSpeed / 10f;

    void Start()
    {
        if (_dialogDataObjects == null || _dialogDataObjects.Length == 0)
        {
            Debug.LogError("DialogData objects are not assigned!");
            return;
        }

        if (_dialogDataObjects[_currentDialogIndex].texts.Count == 0)
        {
            Debug.LogError("DialogData _texts list is empty!");
            return;
        }

        ShowNextText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTyping)
            {
                _typingSpeed = _fastTypingSpeed;
            }
            else
            {
                _currentTextIndex++;
                if (_currentTextIndex < _dialogDataObjects[_currentDialogIndex].texts.Count)
                {
                    ShowNextText();
                }
                else
                {
                    _textUI.text = "Диалог завершён.";
                }
            }
        }
    }

    private void ShowNextText()
    {
        _typingSpeed = 0.05f;
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        var dialogData = _dialogDataObjects[_currentDialogIndex];
        _imageUI = dialogData?.image; // показываем изображение связанное с диалогом
        _typingCoroutine = StartCoroutine(TypeText(dialogData.texts[_currentTextIndex], _textUI));
    }

    private IEnumerator TypeText(string textToType, TextMeshProUGUI textMesh)
    {
        _isTyping = true;
        textMesh.text = "";

        for (int i = 0; i < textToType.Length; i++)
        {
            textMesh.text += textToType[i];
            yield return new WaitForSeconds(_typingSpeed);
        }

        _isTyping = false;
        _typingSpeed = 0.05f;
    }
}