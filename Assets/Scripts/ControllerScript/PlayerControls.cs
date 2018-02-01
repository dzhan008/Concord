using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Player))]
public class PlayerControls : MonoBehaviour {
    //Constants used for Ground Checking and Movement
    private const float groundDepth  = 0.1f;
    private const float groundRadius = 0.1f;
    private const float moveSpeed    = 10.0f;
    private const float jumpSpeed    = 10.0f;
    
    //Delegate for resetting all boolean triggers
    delegate void BooleanDel();
    BooleanDel setBools;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private Player player;
    private ControlScheme controlMap;
    private Rigidbody rigidBody;
    private Animator anim;
    private Vector2 moveDir;
    private float maxMoveDir;
    private bool jumpFlag, attackFlag, interactFlag;

    //Top and Bottom check used in the Ground Check
    private Vector3 groundCheckTop
    {
        get
        {
            return groundCheck.position;
        }
    }

    private Vector3 groundCheckBot
    {
        get
        {
            return new Vector3(
                groundCheck.position.x,
                groundCheck.position.y - groundDepth,
                groundCheck.position.z);
        }
    }

    private void Awake()
    {
        setBools    = () => { };
        moveDir     = Vector2.zero;
        controlMap  = ControlScheme.createControlMap(player.EntID);
        player      = gameObject.GetComponent<Player>();
        rigidBody   = gameObject.GetComponent<Rigidbody>();
        anim        = gameObject.GetComponent<Animator>();
        maxMoveDir  = Vector2.one.magnitude;
    }

    private void Update()
    {
        anim.SetBool("grounded", false);
        //Check if there is ground under the player, add whatIsGround as parameter to check layer
        Collider[] colliders = Physics.OverlapCapsule(groundCheckTop, groundCheckBot, groundRadius);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                //Something was found
                //Debug.Log("Found Ground");
                anim.SetBool("grounded", true);
                break;
            }
        }

        //Parse Input for button press, set flag then add flag to delegate to be unset after InputParse()
        if (Input.GetKeyDown(controlMap.attack))
        {
            attackFlag = true;
            setBools += () => attackFlag = false;
        }

        if (Input.GetKeyDown(controlMap.jump))
        {
            jumpFlag = true;
            setBools += () => jumpFlag = false;
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
        move();
        if (attackFlag)
            attack();

        //Reset all Input flags
        setBools();
        //Set delegate to empty
        setBools = () => { };
    }

    private void move()
    {
        Vector3 dir = new Vector3(moveDir.x, 0.0f, moveDir.y);
        anim.SetFloat("speed", moveDir.magnitude / maxMoveDir);
        if (anim.GetFloat("speed") > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), .15f);
        }

        //Scale the movement by movespeed and maintain current y velocity
        dir *= moveSpeed;
        dir.y = rigidBody.velocity.y;

        if (jumpFlag && anim.GetBool("grounded"))
        {
            anim.SetBool("grounded", false);
            dir.y = jumpSpeed;
        }

        rigidBody.velocity = dir;
    }

    private void attack()
    {
        anim.SetTrigger("attack");
    }
}
