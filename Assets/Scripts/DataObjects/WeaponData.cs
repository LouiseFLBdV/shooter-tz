using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Custom/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _realoadTime;
    [SerializeField] private float _damage;
    [SerializeField] private float _ammoQuantity;
    [SerializeField] private float _attackSpeed;

    public new string name => this._name;
    public float reloadTime => this._realoadTime;
    public float damage => this._damage;
    public float ammoQuantity => this._ammoQuantity;
    public float attackSpeed => this._attackSpeed;
}
