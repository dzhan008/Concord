using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu = 0,
    In_Game,
    Cutscene
}

public class GameManager : MonoBehaviour {

    GameState state;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
