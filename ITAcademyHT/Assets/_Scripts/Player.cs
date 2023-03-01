using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public event Action<float> OnMove;

    [SerializeField] private float _speed = 1f;

    private Controls _controller;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private Vector2 _moveDir = Vector2.zero;

    public float Speed { get { return _speed; } }

    private void Awake()
    {
        _controller = new Controls();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StopMove;
    }

    private void OnDisable()
    {
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StopMove;
        _controller.Disable();
    }

    private void Update()
    {
        OnMove?.Invoke(_moveDir.x);
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Move", true);
        _moveDir.x = obj.ReadValue<float>();
        ChangeViewDirection(_moveDir.x);
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Move", false);
        _moveDir.x = 0;
    }

    private void ChangeViewDirection(float direction) => _sprite.flipX = direction < 0;
}
