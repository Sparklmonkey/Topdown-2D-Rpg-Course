using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : SingletonMono<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentWeapon;
    private PlayerControls _playerControls;
    private bool _attackButtonDown, _isAttacking = false;
    private float _weaponCooldown;

    protected override void Awake()
    {
        base.Awake();
        _playerControls = new();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttack();
        _playerControls.Combat.Attack.canceled += _ => StopAttack();
    }
    
    private void Update()
    {
        if (currentWeapon == null)
        {
            return;
        }
        if (!_isAttacking && _attackButtonDown)
        {
            
            (currentWeapon as IWeapon).Attack();
            StopAllCoroutines();
            StartCoroutine(AttackCooldown());
        }
    }

    public void SetNewWeapon(WeaponInfo newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        if (newWeapon == null)
        {
            currentWeapon = null;
            return;
        }
        var instantiatedWeapon = Instantiate(newWeapon.weaponPrefab, transform.position,
            Quaternion.identity);
        transform.rotation = Quaternion.Euler(0,0,0);
        instantiatedWeapon.transform.parent = transform;
        currentWeapon = instantiatedWeapon.GetComponent<MonoBehaviour>();
        _weaponCooldown = (currentWeapon as IWeapon).GetWeaponInfo().attackCooldown;
    }
    
    private IEnumerator AttackCooldown()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(_weaponCooldown);
        _isAttacking = false;
    }
    private void StartAttack()
    {
        _attackButtonDown = true;
    }

    private void StopAttack()
    {
        _attackButtonDown = false;
    }

    public int GetWeaponDamage()
    {
        return (currentWeapon as IWeapon).GetWeaponInfo().damage;
    }
}
