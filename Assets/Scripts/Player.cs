using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The role that the player chooses to be (warrior, mage, )
    public enum Role
    {
        sword = 0,
        maceAndShield = 1,
        spear = 2,
        dagger = 3,
        bow = 4
    }

public class Player : Entity {

    public Role currentRole;
    public int playerLevel;

    public void Initialize(int ID)
    {
        EntID = ID;
        Blackboard.setPlayerRef(this, EntID);
    }
}