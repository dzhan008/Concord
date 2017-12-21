using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    ControlScheme controlMap;

    [SerializeField]
    Player player;

    bool jump;

    private void Awake()
    {
        controlMap = ControlCreator.createControlMap(player.EntID);
    }

    private void Update()
    {
        jump = false;
        if (Input.GetKeyDown(controlMap.attack)){

        }
        if (Input.GetKeyDown(controlMap.jump))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        player.move(Input.GetAxis("Horizontal" + player.EntID), Input.GetAxis("Vertical" + player.EntID), jump);
    }
}
