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

    [SerializeField]
    private Transform startingPos;

    [SerializeField]
    private Trigger endingPos;

    GameState state;

    private void Awake()
    {
        //endingPos.Initialize(triggerHit);
        //testPlayer.transform.position = startingPos.position;
        //To-do Change this to instantiate all players on start so IDs can correctly be set
        Blackboard.setPlayerRef(testPlayer, 0);
        Blackboard.gameManager = this;
    }
    private void endLevel()
    {
        Time.timeScale = 0;
    }

    public void Incombat()
    {
        Camera.Instance.canMove = false;
    }

    public void OutOfCombat()
    {
        Camera.Instance.canMove = true;
    }
}


