using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    // The role that the player chooses to be (warrior, mage, )
    public enum Role
    {
        sword = 0,
        maceAndShield = 1,
        spear = 2,
        dagger = 3,
        bow = 4
    }

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