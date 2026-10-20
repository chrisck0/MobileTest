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
    
    [SerializeField] private PlayerMovement _playerMovement;
    
    //---------------
    // private void OnEnable() => BindInputActions();
    // private void OnDisable() => UnbindInputActions();
    //---------------

    private void Update() => UpdateVisual(_playerMovement.Direction);
    
    // private void OnMove(InputAction.CallbackContext context)
    // {
    //     Vector2 direction = context.ReadValue<Vector2>();
    //
    //     if (direction == Vector2.zero) _animator.SetBool(_isWalkingId, false);
    //     else _animator.SetBool(_isWalkingId, true);
    //
    //     if (direction.x < 0)
    //     {
    //         _playerVisualTransform.rotation = Quaternion.identity;
    //     }
    //     else if (direction.x > 0)
    //     {
    //         _playerVisualTransform.eulerAngles = new Vector3(0f, 180f, 0f);
    //     }
    // }

    // private void BindInputActions()
    // {
    //     _moveInput.action.Enable();
    //     _moveInput.action.performed += OnMove;
    //     _moveInput.action.canceled += OnMove;
    // }
    //
    // private void UnbindInputActions()
    // {
    //     _moveInput.action.Disable();
    //     _moveInput.action.performed -= OnMove;
    //     _moveInput.action.canceled -= OnMove;
    // }
    
    private void UpdateVisual(Vector3 direction)
    {
        UpdateMoving(direction);
        UpdateDirection(direction);
    }

    private void UpdateMoving(Vector3 direction)
    {
        // 방향 벡터가 0보다 크다 → 입력이 있다. 0인지만 체크 필요하므로 sqr 사용.
        bool isMoving = direction.sqrMagnitude > 0.01f;
        
        _animator.SetBool(_isWalkingId, isMoving);
    }

    private void UpdateDirection(Vector3 direction)
    {
        if (direction.x < 0f)
        {
            _playerVisualTransform.rotation = Quaternion.identity;
        }
        else if (direction.x > 0f)
        {
            _playerVisualTransform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
