using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    Stats enemyStats;

	// Use this for initialization
	void Start () {
        enemyStats = new Stats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        enemyStats.Health -= 50;
        Debug.Log(enemyStats.Health);
        if(enemyStats.Health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
