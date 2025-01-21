using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public KeyCode cambiarArma = KeyCode.Alpha1;

    public List<WeaponController> startingWeapons = new List<WeaponController>(2);

    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;

    public int activeWeaponIndex {  get; private set; }

    private WeaponController[] weaponSlots=new WeaponController[5];

    void Start()
    {
        activeWeaponIndex = -1;

        foreach (WeaponController startingWeapon in startingWeapons)
        {
            AddWeapon(startingWeapon);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(cambiarArma))
        {
            SwitchWeapon();
        }
    }
    private void SwitchWeapon()
    {
        int tempIndex = (activeWeaponIndex + 1) % weaponSlots.Length;

        if (weaponSlots[tempIndex] == null) 
            return;

        foreach(WeaponController weapon in weaponSlots)
        {
            if(weapon != null) weapon.gameObject.SetActive(false);
        }

        weaponSlots[tempIndex].gameObject.SetActive(true);
        activeWeaponIndex = tempIndex;

        EventManager.current.NewGunEvent.Invoke();
    }
    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        for (int i=0; i<weaponSlots.Length;i++)
        {
            if (weaponSlots[i]==null)
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.owner = gameObject;
                weaponClone.gameObject.SetActive(false);

                weaponSlots[i] = weaponClone;
                return;
            }
        }
    }
}