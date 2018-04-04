using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    // Wielder must be one of these roles to wield this weapon
    [SerializeField]
    private Role RoleRestriction;

    // Wielder must be at least this player level to wield this weapon
    [SerializeField]
    private int minPlayerLevel;

    // A reference to the animation that should be played when this weapon is held and REGULAR attack is pressed
    [SerializeField]
    private Animation normalAttackAnimation;

    // A reference to the animation that should be played when this weapon is held and SUPER attack is pressed
    [SerializeField]
    private Animation superAttackAnimation;

    // Damage should be randomized so this is the percent of randomness by which it can vary.
    // After all defense calculations.
    public float damageVariation = 0.10f;

    // How many hits before this weapon breaks
    [SerializeField]
    private int hitsToBreak = 100;
    
    public int hitsTaken {
        get {
            return _hitsTaken;
        }
        set {
            _hitsTaken = Mathf.Clamp(value, 0, hitsToBreak);
        }
    }
    [SerializeField]
    private int _hitsTaken = 0;
    
    public float ultimateMeter {
        get {
            return _ultimateMeter;
        }
        set {
            _ultimateMeter = Mathf.Clamp(value, 0f, 1f);
        }
    }
    [SerializeField]
    private float _ultimateMeter = 0.0f;

    public bool CanWield(Player wielder) {
        if (wielder.currentRole == RoleRestriction 
            && wielder.playerLevel >= minPlayerLevel) {
            return true;
        }
        return false;
    }
    
}