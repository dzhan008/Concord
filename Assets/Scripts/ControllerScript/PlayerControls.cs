using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    delegate void BooleanDel();
    BooleanDel setBools;

    ControlScheme controlMap;

    [SerializeField]
    Player player;

    bool jump;
    bool attack;
    Vector2 moveDir;

    private void Awake()
    {
        controlMap = ControlCreator.createControlMap(player.EntID);
        moveDir = Vector2.zero;
        setBools = () => { };
    }

    private void Update()
    {
        if (Input.GetKeyDown(controlMap.attack))
        {
            attack = true;
            setBools += () => attack = false;
        }

        if (Input.GetKeyDown(controlMap.jump))
        {
            jump = true;
            setBools += () => jump = false;
        }
    }

    private void FixedUpdate()
    {
        moveDir.x = Input.GetAxis("Horizontal" + player.EntID);
        moveDir.y = Input.GetAxis("Vertical" + player.EntID);
        InputParse();
    }

    private void InputParse()
    {
        player.move(moveDir, jump);
        if (attack)
            player.attack();
        setBools();
        setBools = () => { };
    }
}
