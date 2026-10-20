using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private InputActionReference _moveInput;
    private Vector3 _direction;

    /// <summary>
    /// PlayerVisual에서 _direction을 받아 조건 체크에 사용 
    /// </summary>
    public Vector3 Direction => _direction;
    
    //---------------
    private void OnEnable() => BindInputActions();
    private void Update() => Move();
    private void OnDisable() => UnbindInputActions();
    //---------------

    private void Move()
    {
        transform.position += _direction * _playerStat.MovementSpeed * Time.deltaTime;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _direction = new Vector3(direction.x, 0f, direction.y);
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

    /// <summary>
    /// PlayerMovementMobile에서 방향 설정
    /// </summary>
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
