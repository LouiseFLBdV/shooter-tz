using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayShoot : MonoBehaviour
{
    public Camera cam;
    public float damage = 25;
    
    public ParticleSystem shootEffect;
    public ParticleSystem hitEffect;
    public AudioSource shootAudio;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shootEffect.Play();
        shootAudio.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            CharacterController target = hit.transform.GetComponent<CharacterController>();
            if (target != null)
            {
                target.TakeDamage(10);
            }

            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}