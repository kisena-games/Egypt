using UnityEngine;
using DG.Tweening;
using Unity.AI.Navigation;
using System.Collections;
public class WallOpen : MonoBehaviour
{
    [SerializeField] private Transform _wall;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _mummy;
    [SerializeField] private GameObject _portal;
    [SerializeField] private bool _isSpecificalMummy;
    [SerializeField] private bool _isForPortal;
    private void Update()
    {
        
        if (Vector3.Distance(transform.position,_player.position)<1f)
        {
            if (!_isSpecificalMummy)
            {
                transform.DOMoveY(-0.1f, 1f);
                _wall.DOMoveY(-5, 5f);
            }
            if (!_isForPortal)
                _mummy?.SetActive(true);
            else
            {
                _portal.SetActive(true);
            }
        }
        
    }
    

}
