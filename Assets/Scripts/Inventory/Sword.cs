using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject animPrefab, colliderContainer;
    [SerializeField] private Transform slashAnimContainer;
    [SerializeField] private WeaponInfo weaponInfo;
    private Animator _animator;
    private GameObject _slashAnim;

    public WeaponInfo GetWeaponInfo() => weaponInfo;
    public void AttackAnimEndEvent()
    {
        colliderContainer.SetActive(false);
    }
    public void FlipSwingUpAnimationEvent()
    {
        _slashAnim.transform.rotation = Quaternion.Euler(-180,0,0);
        _slashAnim.GetComponent<SpriteRenderer>().flipX = PlayerController.Instance.IsFacingLeft;
    }
    
    public void FlipSwingDownAnimationEvent()
    {
        _slashAnim.transform.rotation = Quaternion.Euler(0,0,0);
        _slashAnim.GetComponent<SpriteRenderer>().flipX = PlayerController.Instance.IsFacingLeft;
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }


    public void Attack()
    {
        _animator.SetTrigger("Attack");
        colliderContainer.SetActive(true);
        _slashAnim = Instantiate(animPrefab, slashAnimContainer.position, Quaternion.identity);
        _slashAnim.transform.parent = transform.parent;
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
