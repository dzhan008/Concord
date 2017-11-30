using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpForce;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundDepth;
    [SerializeField]
    LayerMask whatIsGround;

    private bool isGrounded;
    private float bufferTime;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    public void Initialize(int ID)
    {
        EntID = ID;
        Blackboard.setPlayerRef(this, EntID);
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        Collider[] colliders = Physics.OverlapCapsule(groundCheck.position,
            new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundDepth,
            groundCheck.position.z),
            .1f);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if(colliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
                break;
            }
        }
    }

    public void move(float dir, float vert, bool jump)
    {
        rigidBody.velocity = new Vector3(
            moveSpeed * dir,
            rigidBody.velocity.y,
            moveSpeed * vert);

        //Add animation control once implemented
        if(isGrounded && jump)
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }
}
