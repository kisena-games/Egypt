using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttacking : MonoBehaviour
{
    public static Action OnMummyWait,OnScarabWait;

    [SerializeField] private GameObject _radiuses;
    [SerializeField] private MummyStateMachine _mummyStateMacine;


    private PlayerStateMachine _playerStateMachine;
    private bool _isMummyWait,_isScarabWait,x;
    public bool isSpecificalMummy;
    
    private void OnEnable()
    {
        
        if (isSpecificalMummy)
        {

            _radiuses.SetActive(false);
            _mummyStateMacine.SetSmell(true);
            _mummyStateMacine.SetNoise(false);
            _mummyStateMacine.SetKill(false);
        }
         _playerStateMachine = FindAnyObjectByType<PlayerStateMachine>();
    }
    private void Update()
    {

        if(SceneManager.GetActiveScene().buildIndex >= 7)
        {
            
            if (_playerStateMachine.isStealth
            && !_mummyStateMacine.isFeelPlayerKill
            && !_mummyStateMacine.isFeelPlayerNoise)
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

            if (Input.GetKeyDown(KeyCode.Q) && !_isScarabWait && !_isMummyWait)
            {
                if (_mummyStateMacine.isFeelPlayerNoise)
                {
                    OnMummyWait?.Invoke();
                }
                else
                {
                    OnScarabWait?.Invoke();
                }

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
        if (!_playerStateMachine.isStealth)
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
