using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1;
    public float speed = 2;
    
    public Vector2 turn;
    public float horizontalMove;
    public float verticalMove;
    public Vector3 deltaMove;
    
    public GameObject player;
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
        
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        deltaMove = new Vector3(horizontalMove, 0, verticalMove) * (speed * Time.deltaTime);
        player.transform.Translate(deltaMove);
        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}