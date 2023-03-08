using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : TriggerObject
{
    public delegate void OnTriggerEnter();
    public event OnTriggerEnter OnDeath;

    [SerializeField] private float _speed = 400f;
    [SerializeField] private float _forsePower = 1f;

    private Rigidbody2D _rb;
    private Controller _controller;
    private SpriteRenderer _sprite;
    private Animator _animator;

    private Vector2 _moveDir = Vector2.zero;

    private bool isJumping;

    public Rigidbody2D Rigidbody { get { return _rb; } }

    public TriggerObject TriggerObject { get { return this; } }

    private void Awake()
    {
        _controller = new Controller();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StopMove;
        _controller.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _controller.Player.Jump.performed -= Jump;
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StopMove;
        _controller.Disable();
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null && _rb.velocity.y < 0 && isJumping)
        {
            _animator.SetTrigger("Land");
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = _rb.velocity;
        dir.x = _speed * _moveDir.x;
        _rb.velocity = dir;
    }

    public void PlayerTransform(Vector2 point)
    {
        _rb.position = point;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Run", true);
        _moveDir.x = obj.ReadValue<float>();
        ChangeViewDirection(_moveDir.x);
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Run", false);
        _moveDir.x = 0;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isJumping)
        {
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _forsePower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
        _rb.velocity = Vector2.zero;
    }

    private void ChangeViewDirection(float direction) => _sprite.flipX = direction < 0;

    public override void TriggerActivate()
    {
        Death();
    }
}
