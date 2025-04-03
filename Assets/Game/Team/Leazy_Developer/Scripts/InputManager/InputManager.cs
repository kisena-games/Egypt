using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookDelta { get; private set; } = Vector2.zero;

    public Action JumpAction;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
        }

        MoveInput = context.ReadValue<Vector2>();
        Debug.Log(MoveInput);
    }

    public void OnLookDelta(InputAction.CallbackContext context)
    {

    }
}
