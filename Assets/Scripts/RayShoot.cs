using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RayShoot : MonoBehaviour
{
    public Camera cam;
    public WeaponController[] weapons;
    public ParticleSystem hitEffect;

    private int currentWeaponIndex;
    private float nextFireTime;

    private void Start()
    {
        if (weapons.Length > 0)
        {
            SetActiveWeapon(currentWeaponIndex);
        }
    }

    private void Update()
    {
        HandleWeaponSwitching();
        if (!weapons[currentWeaponIndex].isReloading)
        {
            HandleShooting();
        }
    }

    private void HandleWeaponSwitching()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0)
        {
            SetActiveWeapon(currentWeaponIndex + (scrollWheelInput > 0 ? 1 : -1));
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weapons[currentWeaponIndex].currentAmmo <= 0)
            {
                StartCoroutine(weapons[currentWeaponIndex].Reload());
            } else if (Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
    }

    private void SetActiveWeapon(int newIndex)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        currentWeaponIndex = Mathf.Clamp(newIndex, 0, weapons.Length - 1);
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        weapons[currentWeaponIndex].ChangeWeapon();
    }

    private void Shoot()
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