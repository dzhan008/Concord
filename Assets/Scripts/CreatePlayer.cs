using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour {

    [SerializeField]
    Player playerPrefab;

    public void CreateNewPlayer()
    {
        int id = -1;
        for(int i = 0; i < Blackboard.playerArr.Length; ++i)
        {
            if(Blackboard.playerArr[i] == null)
            {
                id = i;
                break;
            }
        }
        Player playerObj = GameObject.Instantiate(playerPrefab) as Player;
        playerObj.Initialize(id);
        playerObj.transform.position = Vector3.zero;
    }

}
