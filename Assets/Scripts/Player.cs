using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    public CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeet;
    Dash dash;

    public bool isGrounded = false;
    public float moveThrow;
    public float moveSpeed = 5f;
    public int lastPosition = 1; // 1 = right, -1 = left

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Grounded();
    }

    private void Run()
    {
        if (dash.dashState != Dash.DashState.Dashing) // Whenever player is not dashing, player can move with arrow keys (Similar to Hollow Knight)
        {
            moveThrow = Input.GetAxisRaw("Horizontal");
            if(moveThrow != 0)
                lastPosition = (int)moveThrow;
            Vector2 playerVelocity = new Vector2(moveThrow * moveSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
    }

    private void Grounded() // If player is on any platform
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Main"));
    }

    public bool isFalling()
    {
        if (myRigidBody.velocity.y < 0) // Whenever Player starts falling velocity becomes 0 and this returns true
            return true;
        return false;
    }
}
