using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBehaviour : MonoBehaviour
{
    [SerializeField] private float knockbackTime = .2f;
    public bool IsKnockedBack { get; private set; }
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float momentum)
    {
        IsKnockedBack = true;
        Vector2 force = (transform.position - damageSource.position).normalized * momentum * _rigidbody2D.mass;
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EndKnockback());
    }

    private IEnumerator EndKnockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        _rigidbody2D.velocity = Vector2.zero;
        IsKnockedBack = false;
    }
}
