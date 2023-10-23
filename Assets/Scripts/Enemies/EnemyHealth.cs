using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVfx;
    private int _currentHealth;
    private KnockbackBehaviour _knockbackBehaviour;
    private DamageFlash _damageFlash;

    private void Awake()
    {
        _knockbackBehaviour = GetComponent<KnockbackBehaviour>();
        _damageFlash = GetComponent<DamageFlash>();
    }

    private void Start()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(int damage, Transform origin)
    {
        _currentHealth -= damage;
        _knockbackBehaviour.GetKnockedBack(origin, 15f);
        StartCoroutine(_damageFlash.WhiteFlashRoutine(CheckDeath));
    }

    private void CheckDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
