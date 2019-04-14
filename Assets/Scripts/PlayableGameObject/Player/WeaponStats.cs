using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    public float cost;
    public enum WeaponType
    {
        Sword,
        Axe,
        Knife
    }

    public WeaponType weaponType;
}
