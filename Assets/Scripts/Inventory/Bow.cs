using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowOrigin;
    private Animator _animator;

    private readonly int FIRE_HASH = Animator.StringToHash("Fire");
    public void Attack()
    {
        _animator.SetTrigger(FIRE_HASH);
        GameObject arrow = Instantiate(arrowPrefab, arrowOrigin.position, ActiveWeapon.Instance.transform.rotation);
        arrow.GetComponent<Projectile>().SetWeaponInfo(weaponInfo);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;
}
