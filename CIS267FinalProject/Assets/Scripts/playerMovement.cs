using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //movement variables
    public float movementSpeed = 5f;
    public float jumpVelocity = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float inputHorizontal;
    private Rigidbody2D playerRigidBody;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movePlayerLateral();
        jump();
    }

    private void movePlayerLateral()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);

        flipPlayer(inputHorizontal);
    }

    private void flipPlayer(float input)
    {
        if (input > 0)
            transform.eulerAngles = new Vector2(0, 0);

        else if (input < 0)
            transform.eulerAngles = new Vector2(0, 180);
    }

    private void jump()
    {
        //basic jump

        if (Input.GetButtonDown("Jump"))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpVelocity);
        }

        //fast fall

        if (playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //short jump

        else if (playerRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
