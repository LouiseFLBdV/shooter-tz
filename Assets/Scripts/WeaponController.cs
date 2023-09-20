using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioSource shootAudio;
    public AudioSource ReloadAudio;
    public ParticleSystem shootEffect;
    public WeaponData weaponData;
    public string name;
    public float reloadTime;
    public float damage;
    public float ammoQuantity;
    public float currentAmmo;
    public float attackSpeed;
    public bool isReloading = false;
    private void Start()
    {
        name = weaponData.name;
        reloadTime = weaponData.reloadTime;
        damage = weaponData.damage;
        ammoQuantity = weaponData.ammoQuantity;
        currentAmmo = weaponData.ammoQuantity;
        attackSpeed = weaponData.attackSpeed;
    }

    public void Trigger()
    {
        shootEffect.Play();
        shootAudio.Play();
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        ReloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoQuantity;
        isReloading = false;
    }
}