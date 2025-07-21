using UnityEngine;
using UnityEngine.SceneManagement;

public class ForTest : MonoBehaviour
{
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OpenScene(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OpenScene(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OpenScene(10);
        }
    }
}
