using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttacking : MonoBehaviour
{
    public static Action OnMummyWait;

    [SerializeField] private GameObject _radiuses;
    [SerializeField] private MummyStateMachine _mummyStateMacine;
    private PlayerStateMachine _playerStateMachine;
    private bool _isMummyWait,_isScarabWait;

    private void Start()
    {
        _playerStateMachine = FindAnyObjectByType<PlayerStateMachine>();
    }
    private void Update()
    {

        if(SceneManager.GetActiveScene().buildIndex >= 5)
        {
            if (_playerStateMachine._isStealth)
            {
                _radiuses.SetActive(false);
                _mummyStateMacine.SetSmell(true);
                _mummyStateMacine.SetNoise(false);
                _mummyStateMacine.SetKill(false);
            }
            else if(!_isScarabWait && !_isMummyWait)
            {
                _radiuses.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !_isScarabWait && !_isMummyWait)
            {
                OnMummyWait?.Invoke();
                _radiuses.SetActive(false);
                _mummyStateMacine.SetSmell(false);
                _mummyStateMacine.SetNoise(false);
                _mummyStateMacine.SetKill(false);
                StartCoroutine(WaitScarabUse());
                StartCoroutine(WaitMummyAttack());
                _isScarabWait = true;
                _isMummyWait = true;
            }
        }
        
    }
    private IEnumerator WaitMummyAttack()
    {
        yield return new WaitForSeconds(5);
        if (!_playerStateMachine._isStealth)
        {
            _radiuses.SetActive(true);
        }
        _isMummyWait = false;
        Debug.Log("WaitMummyAttack");
    }
    private IEnumerator WaitScarabUse()
    {
        yield return new WaitForSeconds(15);
        
        _isScarabWait = false;
        Debug.Log("WaitScarabUse");
    }

}
