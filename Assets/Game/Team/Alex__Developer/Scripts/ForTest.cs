using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForTest : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPause,_inputManager,_inventory;


    
    private void OpenMenu()
    {
        _canvasPause.SetActive(true);
        _inputManager.SetActive(false);
        _inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0+i))
            {
                SceneManager.LoadScene(i*3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_canvasPause.activeSelf)
                _inventory.SetActive(true);
            else
                OpenMenu();

        }
    }
}
