using UnityEngine;
using UnityEngine.SceneManagement;

public class Spark : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private void Update()
    {
        if(Vector3.Distance(_player.position,transform.position) <0.75f)
        {
            SceneManager.LoadScene(21);
        }
    }
}