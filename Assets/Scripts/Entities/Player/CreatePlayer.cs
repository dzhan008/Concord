using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour {

    [SerializeField]
    Player playerPrefab;

    //Just a temp bool to force a player to not be created again
    bool playerCreated = false;

    public void CreateNewPlayer()
    {
        if (!playerCreated)
        {
            int id = -1;
            for (int i = 0; i < Blackboard.playerArr.Length; ++i)
            {
                if (Blackboard.playerArr[i] == null)
                {
                    id = i;
                    break;
                }
            }
            Player playerObj = GameObject.Instantiate(playerPrefab) as Player;
            playerObj.Initialize(id);
            playerObj.transform.position = Vector3.zero;
            playerCreated = true;
        }

    }

}
