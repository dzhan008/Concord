using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    // Weilder must be one of these roles to weild this weapon
    [SerializeField]
    private Role RoleRestriction;

    // Weilder must be at least this player level to weild this weapon
    [SerializeField]
    private int minPlayerLevel;

    // A reference to the animation that should be played when this weapon is held and REGULAR attack is pressed
    [SerializeField]
    private Animation normalAttackAnimation;

    // A reference to the animation that should be played when this weapon is held and SUPER attack is pressed
    [SerializeField]
    private Animation superAttackAnimation;

    public bool CanWeild(Player weilder) {
        if (weilder.currentRole == RoleRestriction 
            && weilder.PlayerLevel >= minPlayerLevel) {
            return true;
        }
        return false;
    }
	
}