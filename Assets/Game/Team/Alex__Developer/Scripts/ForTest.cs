using UnityEngine;
using UnityEngine.SceneManagement;

public class ForTest : MonoBehaviour
{
    private int _sceneCount;
    private void Start()
    {
        _sceneCount = SceneManager.sceneCount;
    }
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
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
           // SceneManager.LoadScene(0);
        }
    }
}
