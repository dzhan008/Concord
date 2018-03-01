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

    Trigger.OnTrigger triggerHit;

    [SerializeField]
    private Player testPlayer;

    [SerializeField]
    private Transform startingPos;

    [SerializeField]
    private Trigger endingPos;

    GameState state;

    private void Awake()
    {
        triggerHit = endLevel;
        //endingPos.Initialize(triggerHit);
        //testPlayer.transform.position = startingPos.position;
        //To-do Change this to instantiate all players on start so IDs can correctly be set
        Blackboard.setPlayerRef(testPlayer, 0);
    }
    private void endLevel()
    {
        Time.timeScale = 0;
    }
}


