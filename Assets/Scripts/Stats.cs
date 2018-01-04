using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats class defining the basic properties of an entity.
/// </summary>
public class Stats : MonoBehaviour {

    //Health and Ultimate Bar
    public int Health
    {
        get
        {
            return _Health;
        }
        set
        {
            if(value < 0)
            {
                _Health = 0;
            }
            else
            {
                _Health = value;
            }
        }
    }
    private int _Health = 100;

    public int ultimateMeter
    {
        get
        {
            return _ultimateMeter;
        }
        set
        {
            if(value < 0)
            {
                _ultimateMeter = 0;
            }
            else
            {
                _ultimateMeter = value;
            }
        }
    }
    private int _ultimateMeter;


    //General Stats
    private int Strength;
    private int Agility;
    private int Intelligence;

    //Damage
    private int minDamage = 50;
    private int maxDamage = 100;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CalculateDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}
