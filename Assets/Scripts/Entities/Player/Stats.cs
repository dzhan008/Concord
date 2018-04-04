using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats class defining the basic properties of an entity.
/// </summary>
[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class Stats : ScriptableObject {

    //General Stats
    public int Health = 100;
    public int Strength = 10;
    public int Defense = 0;
    public int Agility = 0;
    public int Intelligence = 10;
    public Element Element = Element.none;

}