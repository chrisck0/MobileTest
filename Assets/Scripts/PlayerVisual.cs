using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisual : MonoBehaviour
{
    private const string PARAM_IS_WALKING = "IsWalking";
    private readonly int _isWalkingId = Animator.StringToHash(PARAM_IS_WALKING);
    [SerializeField] private Animator _animator;
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private Transform _playerVisualTransform;

    //---------------
    private void OnEnable() => BindInputActions();
    private void OnDisable() => UnbindInputActions();
    //---------------

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (direction == Vector2.zero) _animator.SetBool(_isWalkingId, false);
        else _animator.SetBool(_isWalkingId, true);

        if (direction.x < 0)
        {
            _playerVisualTransform.rotation = Quaternion.identity;
        }
        else if (direction.x > 0)
        {
            _playerVisualTransform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void BindInputActions()
    {
        _moveInput.action.Enable();
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnMove;
    }

    private void UnbindInputActions()
    {
        _moveInput.action.Disable();
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnMove;
    }
}
