using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
