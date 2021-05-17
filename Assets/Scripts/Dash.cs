using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Player player;
    Rigidbody2D myRigidBody;
    public enum DashState {Ready, Dashing, Cooldown };
    public DashState dashState = DashState.Ready;
    public float dashSpeed = 50f;
    public float maxDashTime = 0.1f;
    public float dashTimer = 0f;
    public float maxDash = 0.5f;
    public float dashCoolDown = 1f;
    public float timer;
    public float dropTimer;
    public float dashToDropTime = 0.015f;
    Vector2 savedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(dashState)
        {

            case DashState.Ready:
                {
                    bool isKeyDown = Input.GetKeyDown(KeyCode.LeftShift); // Whenever left shift is pressed Dashing action is initiated
                    if (isKeyDown)
                    {
                        dashState = DashState.Dashing;
                    }
                    dashTimer = maxDashTime;
                    break;
                }

            case DashState.Dashing:
                {
                    myRigidBody.gravityScale = 0; // While dashing the player is not affect by gravity and moves horizontally
                    dashTimer -= Time.deltaTime;
                    if (dashTimer > 0)
                    {
                        if (player.lastPosition == 1) // If player was facing left in the previous frame, player dashes left, else right
                            myRigidBody.velocity = Vector2.right * dashSpeed; 
                        else
                            myRigidBody.velocity = Vector2.left * dashSpeed;
                    }
                    else
                    {
                        timer = dashCoolDown; 
                        dropTimer = dashToDropTime;
                        dashState = DashState.Cooldown;
                    }
                    break;
                }

            case DashState.Cooldown:
                {
                    dropTimer -= Time.deltaTime; // A drop timer is added so when the player has finished dashing, it stays still for a fraction of time and then falls
                    if(dropTimer < 0)            // This is to ensure smooth falling
                        myRigidBody.gravityScale = 1; // Player is again influenced by gravity
                    timer -= Time.deltaTime;
                    if (timer < 0) // Time until player can dash next
                        dashState = DashState.Ready;
                    break;
                }
        }
    }
}
