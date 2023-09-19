using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float health = 100;
    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _deltaMove;
    private bool _running = false;

    public PlayerHealthBar playerHealthBar;
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        MovePlayer();
        UpdateAnimator();
    }

    private void GetInput()
    {
        _running = Input.GetKey(KeyCode.LeftShift);
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        float actualSpeed = speed + (_running ? speed * 1.5f : 0f);
        _deltaMove = new Vector3(_horizontalMove, 0, _verticalMove) * (actualSpeed * Time.deltaTime);
        transform.Translate(_deltaMove);
    }

    private void UpdateAnimator()
    {
        int moveState = 0;

        if (_verticalMove > 0)
        {
            moveState = _running ? 2 : 1;
        }
        else if (_verticalMove < 0)
        {
            moveState = -1;
        }
        else if (_horizontalMove != 0)
        {
            moveState = 1;
        }

        _playerAnimator.SetInteger("Move", moveState);
    }
    
    public void TakeDamage(float damageTaken)
    {
        if (health > 0)
        {
            health -= damageTaken;
            playerHealthBar.changeHealphBar(health);
            if (health <= 0)
            {
                Destroy(this);
            }
        }
    }
}