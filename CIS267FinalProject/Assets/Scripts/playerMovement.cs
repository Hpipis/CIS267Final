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
    private BoxCollider2D playerBoxCollider2D;
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask groundLayer; //the serialized field adds a dropdown to this scripts in which you can select the layer

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
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
        {
            playerSpriteRenderer.flipX = false;
        }

        else if (input < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
    }

    private void jump()
    {
        //basic jump

        if (Input.GetButtonDown("Jump") && isGrounded())
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

    private bool isGrounded()
    {
        float extraHeight = .1f;
        //Draws a line slightly longer than our box collider to detect if we are grounded
        RaycastHit2D raycastHit = Physics2D.Raycast(playerBoxCollider2D.bounds.center, Vector2.down, playerBoxCollider2D.bounds.extents.y + extraHeight, groundLayer);

        if (raycastHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
