using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _sprintSpeed = 6.5f;
    [SerializeField] private float _speedChangeRate = 10.0f;
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private float _jumpForce = 1.2f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _fallTimeout;
    [SerializeField] private LayerMask _groundLayers;

    private CharacterController _playerController;
    private Camera _mainCamera;
    private StateMachine _stateMachine;
    private bool _isStealth = false;

    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        InitializeStateMachine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (_isStealth)
            {
                if (!IsObstacleAbove())
                {
                    _isStealth = false;
                }
            }
            else
            {
                _isStealth = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isStealth = false;
        }

        _stateMachine.OnUpdate();
    }

    private void InitializeStateMachine()
    {
        State activeState = new ActiveState(_animator, _playerController, _mainCamera, transform, _gravity, _moveSpeed, 
            _sprintSpeed, _speedChangeRate, _jumpForce, _rotationSmoothTime, _fallTimeout, _groundLayers);
        State stealthState = new StealthState(_animator, _playerController, _mainCamera, transform, _moveSpeed,
            _speedChangeRate, _rotationSmoothTime, _groundLayers);

        activeState.AddTransition(new StateTransition(stealthState, new FuncStateCondition(() => _isStealth)));
        stealthState.AddTransition(new StateTransition(activeState, new FuncStateCondition(() => !_isStealth)));
        //stealthState.AddTransition(new StateTransition(activeState, new FuncStateCondition(() => IsSprint)));

        _stateMachine = new StateMachine(activeState);
    }

    private bool IsSprint()
    {
        return InputManager.Instance.IsSprint;
    }

    private bool IsObstacleAbove()
    {
        Vector3 origin = transform.position + Vector3.up * 1.0f;
        float checkHeight = 1.2f;
        return Physics.Raycast(origin, Vector3.up, checkHeight, _groundLayers);
    }
}
