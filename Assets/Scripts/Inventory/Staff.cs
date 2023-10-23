using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour , IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserOrigin;
    private Animator _animator;
    private readonly int FIRE_HASH = Animator.StringToHash("Fire");
    public void Attack()
    {
        _animator.SetTrigger(FIRE_HASH);
    }

    public void SpawnLaserAnimEvent()
    {
        var laser = Instantiate(laserPrefab, laserOrigin.position, Quaternion.identity);
        laser.GetComponent<Laser>().SetRangeAndSpeed(weaponInfo.range, weaponInfo.speed);
    }
    public WeaponInfo GetWeaponInfo() => weaponInfo;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MouseFollowWithOffset();
    }
    
    private void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        ActiveWeapon.Instance.transform.rotation = playerScreenPoint.x > mousePos.x
            ? Quaternion.Euler(0, -180, angle)
            : Quaternion.Euler(0, 0, angle);
    }
}
