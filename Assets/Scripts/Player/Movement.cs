using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    
    private float horizontalMove;
    private float verticalMove;
    private Vector3 deltaMove;
    private bool running = false;
    
    private Animator playerAnimatorTemp;

    private void Start()
    {
        playerAnimatorTemp = GetComponent<Animator>();
    }

    void Update()
    {
        running = Input.GetKey(KeyCode.LeftShift);
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        deltaMove = new Vector3(horizontalMove, 0, verticalMove) * 
                    ((speed + (running ? 1 : 0 * speed * 1.5f)) * Time.deltaTime);
        transform.Translate(deltaMove);
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
