using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
	// Update is called once per frame
    Vector3 center = Vector3.zero;
    Vector3 offset;
    int playerCount = 0;

    private void Awake()
    {
        offset = this.transform.position;
    }

    void FixedUpdate () {
        center = Vector3.zero;
        playerCount = 0;
        for(int i = 0; i < Blackboard.playerArr.Length; ++i)
        {
            if(Blackboard.playerArr[i] != null)
            {
                center += Blackboard.playerArr[i].transform.position;
                playerCount++;
            }
        }
        
        if(playerCount != 0) {
            this.transform.position = offset + (center / playerCount);
        }
    
    }
}
