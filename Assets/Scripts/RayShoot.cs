using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayShoot : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
