using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats class defining the basic properties of an entity.
/// </summary>
public class Stats : MonoBehaviour {

    //Durability and Ultimate Bar
    public float Durability
    {
        get
        {
            return _Durability;
        }
        set
        {
            _Durability = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }
    private float _Durability = 0.0f;

    public float ultimateMeter
    {
        get
        {
            return _ultimateMeter;
        }
        set
        {
            _ultimateMeter = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }
    private float _ultimateMeter = 0.0f;

    //General Stats
    private int Strength;
    private int Agility;
    private int Intelligence;

    //Damage
    private int minDamage = 50;
    private int maxDamage = 100;

    public int CalculateDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}