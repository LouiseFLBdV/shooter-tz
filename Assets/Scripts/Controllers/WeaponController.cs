using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public AudioSource shootAudio;
    public AudioSource ReloadAudio;
    public ParticleSystem shootEffect;
    public WeaponData weaponData;
    public Text texCurrentAmmo;
    
    public bool isReloading = false;
    // public string name;
    public float reloadTime;
    public float damage;
    public float ammoQuantity;
    public float currentAmmo;
    public float attackSpeed;

    private void Start()
    {
        // name = weaponData.name;
        reloadTime = weaponData.reloadTime;
        damage = weaponData.damage;
        ammoQuantity = weaponData.ammoQuantity;
        currentAmmo = weaponData.ammoQuantity;
        attackSpeed = weaponData.attackSpeed;
        texCurrentAmmo.text = currentAmmo + " / " + ammoQuantity;
    }
    
    public void ShootEffect()
    {
        shootEffect.Play();
        shootAudio.Play();
        texCurrentAmmo.text = currentAmmo + " / " + ammoQuantity;
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        ReloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoQuantity;
        isReloading = false;
        texCurrentAmmo.text = currentAmmo + " / " + ammoQuantity;
    }
}