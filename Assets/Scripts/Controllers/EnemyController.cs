using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    public GameObject player;
    public LevelController levelController;

    private Animator _animator;
    private float _health;
    private float _speed;
    private float _damage;
    private int _animationMoveType;
    private float _lastAttackTime;
    private float _attackCooldown;
    private float _detectionPlayer;
    
    private void Start()
    {
        InitializeEnemy();
        _lastAttackTime = Time.time;
    }

    private void Update()
    {
        if (_health > 0)
        {
            HandleMovement();
            HandleAttack();
        }
    }

    private void InitializeEnemy()
    {
        _health = enemyData.health;
        _speed = enemyData.speed;
        _damage = enemyData.damage;
        _attackCooldown = enemyData.attackCooldown;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageTaken)
    {
        if (_health > 0)
        {
            _health -= damageTaken;
            if (_health <= 0)
            {
                StartCoroutine(DieCoroutine());
            }
        }
    }

    private void HandleAttack()
    {
        if (_health > 0 && Vector3.Distance(transform.position, player.transform.position) < 1.1f)
        {
            if (Time.time - _lastAttackTime >= _attackCooldown)
            {
                _animator.SetBool("Attack", true);
                player.GetComponent<PlayerController>().TakeDamage(_damage);
                _lastAttackTime = Time.time;
            }
        }
    }

    private IEnumerator DieCoroutine()
    {
        _animator.SetBool("Die", true);
        yield return new WaitForSeconds(3);
        levelController.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    private void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (_health < enemyData.health)
        {
            _detectionPlayer = 50;
            _speed = enemyData.speed * 1.5f;
        }
        else
        {
            _detectionPlayer = 9;
        }
        if (distanceToPlayer > 1 && distanceToPlayer < _detectionPlayer)
        {
            if (_animationMoveType == 0)
            {
                _animationMoveType = Random.Range(1, 3);
            }

            _animator.SetInteger("Move", _animationMoveType);
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            _animator.SetInteger("Move", 0);
        }
    }
}