using UnityEngine;
using DG.Tweening;
public class WallOpen : MonoBehaviour
{
    [SerializeField] private Transform _wall;
    [SerializeField] private Transform _player;

    private void Update()
    {
        if (Vector3.Distance(transform.position,_player.position)<1f)
        { 
            transform.DOMoveY(-0.1f,1f);
            _wall.DOMoveY(-5, 5f);
        }
    }


}
