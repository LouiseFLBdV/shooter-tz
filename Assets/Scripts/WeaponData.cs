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
    [SerializeField] private float _armoQuantity;

    public new string name => this._name;
    public float reloadTime => this._realoadTime;
    public float damage => this._damage;
    public float armoQuantity => this._armoQuantity;
}
