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
    public float dashDistance = 3f;

    private float inputHorizontal;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerBoxCollider2D;
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask groundLayer; //the serialized field adds a dropdown to this scripts in which you can select the layer

    public Animator animator;

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
        dash();
        //check grounded for animator

        if (isGrounded())
        {
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGrounded", true);
            Debug.Log("Is grounded");
        }
    }

    private void movePlayerLateral()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);

        flipPlayer(inputHorizontal);

        animator.SetFloat("Speed", Mathf.Abs(playerRigidBody.velocity.x));
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
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJumping", true);
            Debug.Log("Is jumping");
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpVelocity);
        }

        //fast fall

        if (playerRigidBody.velocity.y < 0 && !isGrounded())
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
            Debug.Log("Is falling");
        }


        //short jump

        else if (playerRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool canMove(Vector2 direction, float distance)
    {
        //casts a ray in the direction we are moving tot see if we can dash/roll there
        return Physics2D.Raycast(transform.position, direction, distance).collider == null;
    }

    private void dash()
    {
        if(Input.GetKeyDown("j"))
        {
            if (playerSpriteRenderer.flipX == true)
            {
                playerRigidBody.position += new Vector2(-dashDistance, 0);
            }
            else
            {
                playerRigidBody.position += new Vector2(dashDistance, 0);
            }
            
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
