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

    private Stats currentStats;

    private void Awake()
    {
        currentStats = new Stats();
    }

    public void Initialize(int ID)
    {
        EntID = ID;
        Blackboard.setPlayerRef(this, EntID);
    }
}