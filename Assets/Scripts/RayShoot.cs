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
    public WeaponController weaponController;
    private float nextFireTime = 0;
    public Inventory Inventory;
    private void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
    }

    private void Update()
    {
        if (!weaponController.isReloading)
        {
            if (weaponController.currentAmmo <= 0)
            {
                StartCoroutine(weaponController.Reload());
            }
            if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
    }
    
    void Shoot()
    {
        weaponController.currentAmmo--;
        nextFireTime = Time.time + weaponController.attackSpeed;
        weaponController.Trigger();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(weaponController.damage);
            }

            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}