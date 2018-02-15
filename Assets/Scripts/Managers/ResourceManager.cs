/* NAME:            ResourceManager.cs
 * AUTHOR:          Emmilio Segovia
 * DESCRIPTION:     The Resource Manager is meant to keep references of all assets like item
 *                  scriptable objects and Sounds that will need to be accessed. That way,
 *                  everything can on be loaded once in the beginning of the game instead of
 *                  having wait times throughout the game.
 * REQUIREMENTS:    Singleton class must be defined with a static "Instance" variable.
 *                  To avoid typos, item naming convention should mimic book-titles; have each 
 *                  word separate and each word starts with a capital letter except for 
 *                  articles (of, the, a, in). Example: "Oil of a Golem".
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : Singleton<ResourceManager> {
    //the Dictionaries that keep each reference
    private Dictionary<string, Weapon> WeaponDict;
    private Dictionary<string, AudioClip> SoundDict;

    protected void Awake()
    {
        WeaponDict = new Dictionary<string, Weapon>();
        SoundDict = new Dictionary<string, AudioClip>();
        
        LoadElementChart();
		LoadWeapons();
        LoadSounds();
    }

    /// <summary>
    /// Loads element chart.
    /// </summary>
    private void LoadElementChart()
    {
        Blackboard.SetElementChartReference(Resources.Load("ElementChart") as IntersectionMatrix);
    }

    /// <summary>
    /// Loads weapons from the resources folder
    /// </summary>
    private void LoadWeapons()
    {
        object[] loaded_items = Resources.LoadAll("Weapons");
        foreach (object i in loaded_items) {
            GameObject newWep = (GameObject)i;
            if (!WeaponDict.ContainsKey(newWep.name)) {
                WeaponDict.Add(newWep.name, newWep.GetComponent<Weapon>());
            }
        }
    }
    
    /// <summary>
    /// Loads the Audio Clips from the Resources folder
    /// </summary>
    private void LoadSounds()
    {
        object[] loaded_items = Resources.LoadAll("Sounds");
        foreach (AudioClip i in loaded_items) {
            if (!SoundDict.ContainsKey(i.name)) {
                SoundDict.Add(i.name, i);
            }
        }
    }

    public Weapon GetWeapon(string name)
    {
        if (WeaponDict.ContainsKey(name)) {
            return WeaponDict[name];
        }
        else {
            Debug.LogError(name + " not found, it may be mispelled.");
            return null;
        }
    }

    public AudioClip GetSound(string name)
    {
        if (SoundDict.ContainsKey(name)) {
            return SoundDict[name];
        }
        else {
            Debug.LogError(name + " not found, it may be mispelled.");
            return null;
        }
    }
}
