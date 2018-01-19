using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu = 0,
    In_Game,
    Cutscene,
    MAX
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player testPlayer;

    GameState state;

    private void Awake()
    {
        //To-do Change this to instantiate all players on start so IDs can correctly be set
        Blackboard.setPlayerRef(testPlayer, 0);
    }
}
