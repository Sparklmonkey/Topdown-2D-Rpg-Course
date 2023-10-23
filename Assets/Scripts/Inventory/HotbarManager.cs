using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [SerializeField] private List<HotbarItem> hotbarItems;
    private int _activeItemIndex = 1;
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Hotbar.Keyboard.performed += ctx => ToggleActiveItem((int)ctx.ReadValue<float>());
        ToggleActiveItem(1);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void ToggleActiveItem(int index)
    {
        hotbarItems.Find(x => x.name == $"Slot_{_activeItemIndex}").ToggleHightlight(false);
        _activeItemIndex = index;
        hotbarItems.Find(x => x.name == $"Slot_{_activeItemIndex}").ToggleHightlight(true);
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        var weaponObject = hotbarItems.Find(x => x.name == $"Slot_{_activeItemIndex}").GetWeaponInfo();
        ActiveWeapon.Instance.SetNewWeapon(weaponObject);
    }
}
