using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float health = 100;
    public PlayerHealthBar playerHealthBar;
    public LevelController levelController;

    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _deltaMove;
    private bool _running = false;
    private bool _jump;
    private bool isGrounded;
    private Rigidbody _rb;
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
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
        _jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void MovePlayer()
    {
        float actualSpeed = speed + (_running ? speed * 1.5f : 0f);
        
        if (_jump && isGrounded)
        {
            _rb.AddForce(new Vector3(0, 200, 0));
            isGrounded = false;
        }

        _deltaMove = new Vector3(_horizontalMove, 0, _verticalMove) * (actualSpeed * Time.deltaTime);
        transform.Translate(_deltaMove);
    }

    private void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
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
            playerHealthBar.ChangeHealphBar(health);

            if (health <= 0)
            {
                levelController.HandleLose();
            }
        }
    }
}