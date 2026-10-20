using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobilePress : MonoBehaviour
{
    [SerializeField] private InputActionReference _pressAction;
    public event Action OnPress;
    public event Action OnPressCancel;

    private void OnEnable() => BindInputActions();
    private void OnDisable() => UnbindInputActions();

    private void OnPressPerformed(InputAction.CallbackContext context)
    {
        OnPress?.Invoke();
    }

    private void OnPressCanceled(InputAction.CallbackContext context)
    {
        OnPressCancel?.Invoke();
    }

    private void BindInputActions()
    {
        _pressAction.action.Enable();
        _pressAction.action.performed += OnPressPerformed;
        _pressAction.action.canceled += OnPressCanceled;
    }

    private void UnbindInputActions()
    {
        _pressAction.action.Disable();
        _pressAction.action.performed -= OnPressPerformed;
        _pressAction.action.canceled -= OnPressCanceled;
    }
}
