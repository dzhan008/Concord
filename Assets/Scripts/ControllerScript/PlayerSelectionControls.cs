using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionControls : MonoBehaviour {

    // Character Selection
    private CharacterManager characterManager;
    private Player player;
    enum CharSelectStates {Selecting, Press, Held, Release, Selected, Finished};
    CharSelectStates charSelect = CharSelectStates.Selecting;
    bool positive = false;
    
    private void Start ()
    {
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        player = gameObject.GetComponent<Player>();
    }


    private void FixedUpdate()
    {
        switch (charSelect)
        {
            case CharSelectStates.Selecting:
                if (Input.GetButtonDown("Joystick" + player.EntID + "X"))
                {
                    Debug.Log("Player" + player.EntID + " ready.");
                    characterManager.readyPlayer(player.EntID);
                    charSelect = CharSelectStates.Selected;
                }

                if (Input.GetAxis("Vertical" + player.EntID) > 0)
                {
                    charSelect = CharSelectStates.Press;
                    positive = true;
                }

                if (Input.GetAxis("Vertical" + player.EntID) < 0)
                {
                    charSelect = CharSelectStates.Press;
                    positive = false;
                }
                break;
            case CharSelectStates.Press:
                characterManager.playerModels[player.EntID].GetComponent<CharacterSelection>().SetPlayer(player.EntID, positive);
                characterManager.playerModels[player.EntID].GetComponent<CharacterSelection>().SetPlayerSplash(player.EntID);
                characterManager.playerModels[player.EntID].GetComponent<CharacterSelection>().SetPlayerStats(player.EntID);
                charSelect = CharSelectStates.Held;
                break;
            case CharSelectStates.Held:
                if (Input.GetAxis("Vertical" + player.EntID) == 0)
                {
                    charSelect = CharSelectStates.Release;
                }
                break;
            case CharSelectStates.Selected:
                if (Input.GetButtonDown("Joystick" + player.EntID + "O"))
                {
                    Debug.Log("Player" + player.EntID + " unready.");
                    characterManager.unreadyPlayer(player.EntID);
                    charSelect = CharSelectStates.Selecting;
                }
                break;
            case CharSelectStates.Finished:

                break;
            case CharSelectStates.Release:
                charSelect = CharSelectStates.Selecting;
                break;

        }
    }
}
