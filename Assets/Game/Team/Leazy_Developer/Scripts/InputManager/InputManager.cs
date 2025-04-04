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

    public Vector2 MoveInputNormalized { get; private set; } = Vector2.zero;
    public bool IsMoving { get; private set; } = false;
    public bool IsSprint { get; private set; } = false;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMoving = true;
            MoveInputNormalized = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            IsMoving = false;
            MoveInputNormalized = Vector2.zero;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSprint = true;
        }
        else if (context.canceled)
        {
            IsSprint = false;
        }
    }
}
