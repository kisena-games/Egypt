using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Escape))
        {
            GameFinish();
        }
    }
    private void GameFinish()
    {
        SceneManager.LoadScene(0);
    }
}
