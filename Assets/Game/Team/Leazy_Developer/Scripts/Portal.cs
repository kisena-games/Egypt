using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _sceneIndex = 1;
    [SerializeField] private GameObject _portalParticles;
    [SerializeField] private Dialog _dialog;
    private BoxCollider _boxCollider;
    private void Awake()
    {/*
        _portalParticles.SetActive(false);
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;*/
    }
    private void OnEnable()
    {
        if (_dialog != null)
            _dialog.OnDialogComplete += OnDialogFinished;
    }
    private void OnDisable()
    {
        if(_dialog != null)
            _dialog.OnDialogComplete -= OnDialogFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerListener>())
        {
            LoadNextLevel();
        }
    }
    private void OnDialogFinished()
    {
        _portalParticles.SetActive(true);
        _boxCollider.enabled = true;
    }
    private void LoadNextLevel()
    {
        if (_sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
