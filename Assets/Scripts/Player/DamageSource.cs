using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int _damageAmount;

    private void Start()
    {
        _damageAmount = ActiveWeapon.Instance.GetWeaponDamage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (!enemyHealth)
        {
            return;
        }
        enemyHealth.TakeDamage(_damageAmount, transform);
    }
}
