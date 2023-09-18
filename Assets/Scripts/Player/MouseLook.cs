using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1;

    public float speed = 2;

    //  (speed + (running?1:0 * speed * 1.5f)
    private bool running = false;

    public Vector2 turn;
    public float horizontalMove;
    public float verticalMove;
    public Vector3 deltaMove;

    public GameObject player;
    private Animator playerAnimatorTemp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerAnimatorTemp = player.GetComponent<Animator>();
    }

    void Update()
    {
        running = Input.GetKey(KeyCode.LeftShift);
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        turn.y = Mathf.Clamp(turn.y, -90, 90);
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        deltaMove = new Vector3(horizontalMove, 0, verticalMove) *
                    ((speed + (running ? 1 : 0 * speed * 1.5f)) * Time.deltaTime);
        player.transform.Translate(deltaMove);
        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        tempAnimatorController();
    }

    void tempAnimatorController()
    {
        if (verticalMove > 0)
        {
            playerAnimatorTemp.SetInteger("Move", 1);
            if (running)
            {
                playerAnimatorTemp.SetInteger("Move", 2);
            }
        }
        else if (verticalMove < 0)
        {
            Debug.Log(verticalMove);
            playerAnimatorTemp.SetInteger("Move", -1);
        }
        else if (horizontalMove != 0)
        {
            playerAnimatorTemp.SetInteger("Move", 1);
        }
        else
        {
            playerAnimatorTemp.SetInteger("Move", 0);
        }
    }
}