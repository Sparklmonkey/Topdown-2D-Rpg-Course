using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [SerializeField] private GameObject activeHighlight;
    [SerializeField] private Image activeItemImage;
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo() => weaponInfo;
    public void ToggleHightlight(bool active)
    {
        activeHighlight.SetActive(active);
    }
}
