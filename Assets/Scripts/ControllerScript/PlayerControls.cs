using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Player))]
public class PlayerControls : MonoBehaviour
{
    //Constants used for Ground Checking and Movement
    private const float groundDepth = 0.1f;
    private const float groundRadius = 0.1f;
    private const float moveSpeed = 10.0f;
    private const float jumpSpeed = 10.0f;

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
    public PlayerInfo MyPlayerInfo; // Set by UIManager
    private Vector3 moveDir;
    private float maxMoveDir;
    private bool jumpFlag, attackFlag, interactFlag;
    private KeyCode attackType;

    //Knockback stuff
    [SerializeField]
    private float knockBackForce = 1;
    [SerializeField]
    public float knockBackTime = 1;
    private float knockBackCounter;

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

    private void Start()
    {
        setBools = () => { };
        moveDir = Vector3.zero;
        player = gameObject.GetComponent<Player>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        controlMap = ControlScheme.createControlMap(player.EntID);
        maxMoveDir = Vector2.one.magnitude;
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
        if (Input.GetKeyDown(controlMap.lightAttack))
        {
            attackFlag = true;
            setBools += () => attackFlag = false;
            attackType = controlMap.lightAttack;
        }
        else if (Input.GetKeyDown(controlMap.strongAttack))
        {
            attackFlag = true;
            setBools += () => attackFlag = false;
            attackType = controlMap.strongAttack;

        }

        if (Input.GetKeyDown(controlMap.jump))
        {
            jumpFlag = true;
            setBools += () => jumpFlag = false;
        }

        if (Input.GetKeyDown(controlMap.InventoryScrollLeft))
        {
            MyPlayerInfo.ScrollLeft();
        }

        if (Input.GetKeyDown(controlMap.InventoryScrollRight))
        {
            MyPlayerInfo.ScrollRight();
        }

        if (Input.GetKeyDown(controlMap.InventoryUseItem))
        {
            MyPlayerInfo.UseItem();
        }
        Vector3 pos = UnityEngine.Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = UnityEngine.Camera.main.ViewportToWorldPoint(pos);
    }

    private void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            moveDir.x = Input.GetAxis("Horizontal" + player.EntID);
            moveDir.z = Input.GetAxis("Vertical" + player.EntID);
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
        InputParse();
    }

    private void InputParse()
    {
        if (attackFlag && player.state != PlayerStates.moving)
            attack();
        if (player.state != PlayerStates.attacking)
            move();
        //Reset all Input flags
        setBools();
        //Set delegate to empty
        setBools = () => { };
    }

    private void move()
    {
        Vector3 dir = new Vector3(moveDir.x, 0.0f, moveDir.z);
        anim.SetFloat("speed", moveDir.magnitude / maxMoveDir);
        if (anim.GetFloat("speed") > 0 && knockBackCounter <= 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), .15f);
        }

        if(dir == Vector3.zero)
        {
            player.state = PlayerStates.idle;
        }
        else
        {
            player.state = PlayerStates.moving;
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
        //Attack based off of the type of attack
        anim.SetTrigger("Attack");
        if (attackType == controlMap.lightAttack)
        {
            anim.SetTrigger("LightAttack");
        }
        else if (attackType == controlMap.strongAttack)
        {
            anim.SetTrigger("StrongAttack");
        }
        player.state = PlayerStates.attacking;
    }

    public void DisableInput()
    {
        anim.SetBool("DisableTransitions", true);
    }

    public void EnableInput()
    {
        anim.SetBool("DisableTransitions", false);
    }

    public void KnockBack(Vector3 dir)
    {
        Debug.Log("Knocking Back!");
        knockBackCounter = knockBackTime;
        moveDir = knockBackForce * dir;
        //Attempt to test knockback on y-axis
        //moveDir = knockBackForce * (Quaternion.AngleAxis(-30, Vector3.forward) * dir);
    }

}