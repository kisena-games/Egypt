using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private List<string> _questions;
    [SerializeField] private List<string> _answers;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _answerText;

    private int _index = 0;
    private Coroutine _typingCoroutine;
    private bool _isTyping = false;
    private bool _showingQuestion = true;

    private float _typingSpeed = 0.05f;
    private float _fastTypingSpeed => _typingSpeed / 10f;

    void Start()
    {
        if (_questions.Count == 0 || _answers.Count == 0 || _questions.Count != _answers.Count)
        {
            Debug.LogError("Questions and Answers lists must be set and have the same length!");
            return;
        }
        _answerText.text = "";
        ShowNextQuestion();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ или ПКМ можно добавить
        {
            if (_isTyping)
            {
                // Ускоряем анимацию если она идёт
                _typingSpeed = _fastTypingSpeed;
            }
            else
            {
                // Переключаемся между вопросом и ответом
                if (_showingQuestion)
                {
                    ShowAnswer();
                }
                else
                {
                    _index++;
                    if (_index < _questions.Count)
                    {
                        ShowNextQuestion();
                    }
                    else
                    {
                        // Диалог окончен
                        _questionText.text = "";
                        _answerText.text = "Диалог завершён.";
                    }
                }
            }
        }
    }

    private void ShowNextQuestion()
    {
        _typingSpeed = 0.05f;
        _showingQuestion = true;
        _answerText.text = "";
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeText(_questions[_index], _questionText));
    }

    private void ShowAnswer()
    {
        _typingSpeed = 0.05f;
        _showingQuestion = false;
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeText(_answers[_index], _answerText));
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
        _typingSpeed = 0.05f; // Сбрасываем скорость в норму для следующих
    }
}