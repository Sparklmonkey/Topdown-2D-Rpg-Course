using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMono<PlayerController>
{
    public bool IsFacingLeft { get; set; }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldownTime = 0.25f;
    [SerializeField] private TrailRenderer trailRenderer;

    private float _startingMoveSpeed;
    private bool _canDash = true;
    private SpriteRenderer _playerSprite;
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private KnockbackBehaviour _knockback;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        
        _knockback = GetComponent<KnockbackBehaviour>();
        _playerControls = new();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.started += _ => PlayerDash();
        _startingMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        if (_knockback.IsKnockedBack)
        {
            return;
        }
        MovePlayer();
        AdjustPlayerSprite();
    }

    private void MovePlayer()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerSprite()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        
        _playerSprite.flipX = IsFacingLeft = playerScreenPoint.x > mousePos.x;
    }

    private void PlayerDash()
    {
        if (!_canDash)
        {
            return;
        }

        _canDash = false;
        moveSpeed *= dashSpeed;
        trailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = _startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldownTime);
        _canDash = true;
    }
}
