using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDepth;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private float bufferTime;
    private Rigidbody rigidBody;
    private Animator anim;

    private Stats currentStats;


    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        anim  = this.GetComponent<Animator>();
        currentStats = new Stats();
    }

    public void Initialize(int ID)
    {
        EntID = ID;
        Blackboard.setPlayerRef(this, EntID);
    }

    private void Update()
    {
        isGrounded = false;
        Vector3 groundEnd = groundCheck.position;
        groundEnd.y -= groundDepth;
        Collider[] colliders = Physics.OverlapCapsule(groundCheck.position, groundEnd, groundRadius);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if(colliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
                break;
            }
        }
        anim.SetBool("grounded", isGrounded);
    }

    //@to-do Clean-up the velocity set, rotation set
    public void move(Vector2 moveDir, bool jump)
    {
        Vector3 Dir = new Vector3(moveDir.x, 0.0f, moveDir.y);
        bool moving = moveDir.magnitude > 0;
        anim.SetBool("moving", moving);
        if(moving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Dir), .15f);
        }

        //Scale the movement by movespeed and maintain current y velocity
        Dir *= moveSpeed;
        Dir.y = rigidBody.velocity.y;
        rigidBody.velocity = Dir;

        if (isGrounded && jump && anim.GetBool("grounded"))
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }

    public void attack()
    {
        anim.SetTrigger("attack");
    }
}
