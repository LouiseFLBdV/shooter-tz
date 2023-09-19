using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioSource shootAudio;
    public ParticleSystem shootEffect;
    public WeaponData WeaponData;
    public void Trigger()
    {
        shootEffect.Play();
        shootAudio.Play();
    }
}