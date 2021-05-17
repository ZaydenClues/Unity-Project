using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Player player;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D playerBody;

    public bool doubleJump;
    public bool isJumping;
    int counter = 1;
    [SerializeField] float jumpTimer = 0f;
    [SerializeField] float jumpSpeed = 0.2f;
    [SerializeField] float jumpTime = 0.5f;
    [SerializeField] float doubleJumpSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        myRigidBody = GetComponent<Rigidbody2D>();
        playerBody = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }

    private void PlayerJump()
    {
        if (player.isGrounded)
            counter = 1; // Number of times player can jump mid air
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded) // Press Space to initiate jump action
        {
            isJumping = true;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 5);
            jumpTimer = jumpTime;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true) // Holding Jump varies the jump distance
        {
            if (jumpTimer > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
                jumpTimer -= Time.deltaTime;
            }
            else
                isJumping = false;
        }
        if(!player.isGrounded && counter == 1 && isJumping == false && Input.GetKeyDown(KeyCode.Space)) // For Double Jump
        {
            myRigidBody.velocity = new Vector2(player.moveThrow * player.moveSpeed, jumpSpeed + doubleJumpSpeed);
            counter -= 1;
        }
        if (Input.GetKeyUp(KeyCode.Space)) // When space key is not pressed anymore
        {
            isJumping = false;
        }
        if(playerBody.IsTouchingLayers(LayerMask.GetMask("Main"))) // If object is touching any other platform, it is not falling anymore
        {
            isJumping = false;
        }
        if(player.isFalling()) // For the object to fall at constant speed
        {
            myRigidBody.gravityScale = 0.5f; // Gravity Scale is reduced to make the object fall a bit more smoother
            if (myRigidBody.velocity.y <= -25) // This will make sure the falling velocity stays constant at 25m/s downwards
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -25);
        }
        else
        {
            myRigidBody.gravityScale = 1f;
        }
    }

}
