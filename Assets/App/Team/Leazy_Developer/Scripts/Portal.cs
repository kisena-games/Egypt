using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _sceneIndex = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerListener>())
        {
            LoadNextLevel();
        }
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
