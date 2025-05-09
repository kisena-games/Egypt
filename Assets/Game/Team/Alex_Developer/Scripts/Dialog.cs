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

    private bool _isPlayerTurn = false;

    void Start()
    {
        var dialogData = _dialogDataObjects[_currentDialogIndex];
        if (dialogData.texts.Count == 0 || dialogData.playerTexts.Count == 0)
        {
            Debug.LogError("DialogData _texts or _playerTexts list is empty!");
            return;
        }
        if (dialogData.texts.Count != dialogData.playerTexts.Count)
        {
            Debug.LogWarning("Количество вопросов и ответов не совпадает!");
        }

        _currentTextIndex = 0;
        _isPlayerTurn = false; 
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
                if (!_isPlayerTurn)
                {
                    _isPlayerTurn = true;
                    ShowNextText();
                }
                else
                {
          
                    _currentTextIndex++;
                    _isPlayerTurn = false;
                    var dialogData = _dialogDataObjects[_currentDialogIndex];

                    if (_currentTextIndex < dialogData.texts.Count)
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
    }

    private void ShowNextText()
    {
        _typingSpeed = 0.05f;

        var dialogData = _dialogDataObjects[_currentDialogIndex];
        if (!_isPlayerTurn)
        {
            //_imageUI = dialogData.image; // изображение собеседника
            _typingCoroutine = StartCoroutine(TypeText(dialogData.texts[_currentTextIndex], _textUI));


            _textUI.color= Color.red;
            _imageUI.color = Color.red;
        }
        else
        {
            _typingCoroutine = StartCoroutine(TypeText(dialogData.playerTexts[_currentTextIndex], _textUI));


            _textUI.color = Color.blue;
            _imageUI.color = Color.blue;
        }
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