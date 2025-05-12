using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Dialog : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private DialogData[] _dialogDataObjects; // два объекта ScriptableObject
    [Header("Place for image and text")]
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Image _imageUI;
    [Header("The index of the dialog from the list 'Dialog Data Objects'")]
    [SerializeField] private int _currentDialogIndex = 0; // индекс текущего объекта
    [Header("Image color and text color 1st the side")]
    [SerializeField] private Color _imageColorFirst=Color.red;
    [SerializeField] private Color _textColorFirst = Color.red;
    [Header("Image color and text color 2nd the side")]
    [SerializeField] private Color _imageColorSecond=Color.blue;
    [SerializeField] private Color _textColorSecond = Color.blue;

    private Coroutine _typingCoroutine;

    private float _typingSpeed = 0.05f;
    private float _fastTypingSpeed => _typingSpeed / 10f;

    private int _currentTextIndex = 0;
    
    

    private bool _isTyping = false;
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
        if (Input.GetMouseButtonDown(0) && _currentDialogIndex <= _dialogDataObjects.Length)
        {

            var dialogData = _dialogDataObjects[_currentDialogIndex];

            if (_isTyping)
            {
                _typingSpeed = _fastTypingSpeed;
            }
            else
            {

                if (!_isPlayerTurn && _currentTextIndex < dialogData.texts.Count)
                {
                    _isPlayerTurn = true;
                    ShowNextText();
                }
                else
                {
                    _currentTextIndex++;
                    _isPlayerTurn = false;

                    if (_currentTextIndex < dialogData.texts.Count)
                    {
                        ShowNextText();
                    }
                    else if (_currentTextIndex == dialogData.texts.Count)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        else Debug.Log("Число _currentDialogIndex больше кол-ва добавленных к скрипту _scriptable объектов!");
    }

    private void ShowNextText()
    {
        _typingSpeed = 0.05f;

        var dialogData = _dialogDataObjects[_currentDialogIndex];
        if (!_isPlayerTurn)
        {
            _imageUI.sprite = dialogData?.image; 
            _typingCoroutine = StartCoroutine(TypeText(dialogData.texts[_currentTextIndex], _textUI));


            _textUI.color = _textColorFirst;
            _imageUI.color = _imageColorFirst;
        }
        else
        {
            _typingCoroutine = StartCoroutine(TypeText(dialogData.playerTexts[_currentTextIndex], _textUI));


            _textUI.color = _textColorSecond;
            _imageUI.color = _imageColorSecond;
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