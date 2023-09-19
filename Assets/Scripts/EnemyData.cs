using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Custom/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;

    public new string name => this._name;
    public float health => this._health;
    public float speed => this._speed;
    public float damage => this._damage;
    public float attackCooldown => this._attackCooldown;
}