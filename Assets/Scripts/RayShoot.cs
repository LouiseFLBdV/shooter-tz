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

    private void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        weaponController.Trigger();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(10);
            }

            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}