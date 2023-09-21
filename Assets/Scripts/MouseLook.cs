using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1;
    public GameObject player;
    private Vector2 turn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        turn.y = Mathf.Clamp(turn.y, -90, 90);
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}