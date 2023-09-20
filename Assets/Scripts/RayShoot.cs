using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RayShoot : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem hitEffect;
    public WeaponController[] weapons;
    private float nextFireTime = 0;
    public Inventory Inventory;
    public int currentWeaponIndex = 0;

    private void Start()
    {
        if (weapons.Length > 0)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex += (scrollWheelInput > 0) ? 1 : -1;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weapons.Length - 1;
            }
            else if (currentWeaponIndex >= weapons.Length)
            {
                currentWeaponIndex = 0;
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }

        if (!weapons[currentWeaponIndex].isReloading)
        {
            if (weapons[currentWeaponIndex].currentAmmo <= 0)
            {
                StartCoroutine(weapons[currentWeaponIndex].Reload());
            }

            if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        weapons[currentWeaponIndex].currentAmmo--;
        nextFireTime = Time.time + weapons[currentWeaponIndex].attackSpeed;
        weapons[currentWeaponIndex].ShootEffect();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(weapons[currentWeaponIndex].damage);
            }

            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}