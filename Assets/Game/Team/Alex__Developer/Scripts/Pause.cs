
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _inputManager;
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _inputManager.SetActive(true);
            CloseMenu();
            
        }
    }
    public void CloseMenu()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }
}
