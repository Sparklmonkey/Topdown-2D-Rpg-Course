using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackThrust = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private bool _canTakeDamage = true;
    private int _currentHealth;
    private DamageFlash _damageFlash;
    private KnockbackBehaviour _knockback;

    private void Awake()
    {
        _knockback = GetComponent<KnockbackBehaviour>();
        _damageFlash = GetComponent<DamageFlash>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyAI>();
        if (enemy && _canTakeDamage)
        {
            _canTakeDamage = false;
            _knockback.GetKnockedBack(enemy.gameObject.transform, knockbackThrust);
            StartCoroutine(_damageFlash.WhiteFlashRoutine(CheckDeath));
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }
    
    private void CheckDeath()
    {
        if (_currentHealth <= 0)
        {
            // Instantiate(deathVfx, transform.position, Quaternion.identity);
            // Destroy(gameObject);
        }
    }
}
