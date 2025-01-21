using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject weaponInfoPrefab;

    private void Start()
    {
        EventManager.current.NewGunEvent.AddListener(CreateWeaponInfo);
    }
    public void CreateWeaponInfo()
    {
        Instantiate(weaponInfoPrefab,transform);
    }
}