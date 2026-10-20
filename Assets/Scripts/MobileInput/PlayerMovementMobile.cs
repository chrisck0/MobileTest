using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementMobile : MonoBehaviour
{
    [SerializeField] private InputActionReference _touchPositionAction;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MobilePress _mobilePress;
    [SerializeField] private float _minThreshold;
    [SerializeField] private float _maxThreshold;
    private Vector2 _initalPosition;
    private bool _isPressing;
    private bool _isInitialPress;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnbindEvents();
    private void Update() => DetectPosition();

    private void DetectPosition()
    {
        if (!_isPressing) return;

        if (_isInitialPress)
        {
            _initalPosition = _touchPositionAction.action.ReadValue<Vector2>();
            _isInitialPress = false;
        }

        Vector2 input = _touchPositionAction.action.ReadValue<Vector2>() - _initalPosition;
        Debug.Log($"Input : {input}, Input Size : {input.magnitude}");
        if (input.magnitude > _maxThreshold)
        {
            input.Normalize();
        }
        else if (input.magnitude < _minThreshold)
        {
            input = Vector2.zero;
        }
        else {
            input /= _maxThreshold;
            _playerMovement.SetDirection(Vector3.zero);
        }
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        _playerMovement.SetDirection(direction);
    }

    private void OnPressPerformed()
    {
        _isPressing = true;
        _isInitialPress = true;
    }

    private void OnPressCanceled()
    {
        _isPressing = false;
        _playerMovement.SetDirection(Vector3.zero);
    }

    private void BindEvents()
    {
        BindInputActions();
        BindMobilePressEvents();
    }

    private void UnbindEvents()
    {
        UnbindInputActions();
        UnbindMobilePressEvents();
    }

    private void BindInputActions()
    {
        _touchPositionAction.action.Enable();
    }

    private void UnbindInputActions()
    {
        _touchPositionAction.action.Disable();
    }

    private void BindMobilePressEvents()
    {
        _mobilePress.OnPress += OnPressPerformed;
        _mobilePress.OnPressCancel += OnPressCanceled;
    }

    private void UnbindMobilePressEvents()
    {
        _mobilePress.OnPress -= OnPressPerformed;
        _mobilePress.OnPressCancel -= OnPressCanceled;
    }
}
