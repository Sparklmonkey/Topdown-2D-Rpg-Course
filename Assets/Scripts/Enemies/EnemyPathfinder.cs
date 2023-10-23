using System;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _destination;
    private KnockbackBehaviour _knockbackBehaviour;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _knockbackBehaviour = GetComponent<KnockbackBehaviour>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_knockbackBehaviour.IsKnockedBack)
        {
            return;
        }
        _rigidbody2D.MovePosition(_rigidbody2D.position + _destination * (moveSpeed * Time.fixedDeltaTime));
        _spriteRenderer.flipX = _destination.x < 0;
    }

    public void MoveToPosition(Vector2 destination)
    {
        _destination = destination;
    }
}
