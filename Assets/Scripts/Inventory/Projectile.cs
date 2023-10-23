using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 22f;
    [SerializeField] private GameObject projectileVfxPrefab;

    private WeaponInfo _weaponInfo;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    public void SetWeaponInfo(WeaponInfo weaponInfo)
    {
        _weaponInfo = weaponInfo;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
        CheckProjectileRange();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        var indestructible = other.gameObject.GetComponent<Indestructible>();

        if (!other.isTrigger && (enemyHealth || indestructible))
        {
            Instantiate(projectileVfxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void CheckProjectileRange()
    {
        if (Vector3.Distance(transform.position, _startPos) > _weaponInfo.range)
        {
            Instantiate(projectileVfxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
