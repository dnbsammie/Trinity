using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FireMode
{
    SemiAutomatic,
    FullAutomatic
}
public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public Transform weaponMuzzle;

    [Header("General")]
    public KeyCode Disparo = KeyCode.Mouse0;
    public KeyCode Recargar = KeyCode.R;
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    [Header("Weapon Parameters")]
    public FireMode fireMode;
    public float fireRate = 0.6f;
    public int capacity = 30;
    public float wallPenetration = 0;
    public float fireRange = 200;
    public float headDamage = 150;
    public float bodyDamage = 50;
    public float legsDamage = 35;
    public float recoilForce = 5f;
    public float reloadTime = 1.5f;

    public int currentAmmo { get; private set; }

    private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Sounds & Visuals")]
    public GameObject flashEffect;

    public GameObject owner { get; set; }

    private Transform cameraPlayerTransform;

    private void Awake()
    {
        currentAmmo = capacity;
        EventManager.current.updateBulletsEvent.Invoke(currentAmmo, capacity);
    }
    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }   
    private void Update()
    {
        if(fireMode == FireMode.SemiAutomatic)
        {
            if (Input.GetKeyDown(Disparo))
            {
                TryShoot();
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        }
        else if(fireMode == FireMode.FullAutomatic)
        {
            if (Input.GetKey(Disparo))
            {
                TryShoot();
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);

        }

        if(Input.GetKeyDown(Recargar))
        {
            StartCoroutine(Reload());
        }
    }
    private bool TryShoot()
    {
        if (lastTimeShoot+fireRate<Time.time)
        {
            if (currentAmmo >= 1)
            {
                HandleShoot();
                currentAmmo -= 1;
                EventManager.current.updateBulletsEvent.Invoke(currentAmmo, capacity);
                return true;
            }
        }

        return false;
    }
    private void HandleShoot()
    {
        GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
        Destroy(flashClone, 1f);

        AddRecoil();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(cameraPlayerTransform.position, cameraPlayerTransform.forward, fireRange, hittableLayers);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 4f);
            }
        }

        lastTimeShoot = Time.time;
    }
    private void AddRecoil()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
    }
    IEnumerator Reload()
    {
        Debug.Log("recargando xd");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = capacity;
        EventManager.current.updateBulletsEvent.Invoke(currentAmmo, capacity);
        Debug.Log("tolis");
    }
}