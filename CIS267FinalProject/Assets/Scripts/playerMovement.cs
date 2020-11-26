﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //movement variables
    public float movementSpeed = 5f;
    public float jumpVelocity = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //sound variables
    public AudioClip playerJumpSound;
    private AudioSource audioSource;

    private bool canJump = true;
    private bool attack;
    private float inputHorizontal;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerBoxCollider2D;
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask groundLayer; //the serialized field adds a dropdown to this scripts in which you can select the layer

    public Animator animator;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        movePlayerLateral();
        jump();

        //attackInput();

        //check grounded for animator

        if (isGrounded())
        {
            animator.SetBool("IsFalling", false);
            if (playerRigidBody.velocity.y <= 0)
            {
                animator.SetBool("IsJumping", false);
            }
            animator.SetBool("IsGrounded", true);
            Debug.Log("Is grounded");
        }
    }

    private void movePlayerLateral()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);
        }

        inputHorizontal = Input.GetAxisRaw("Horizontal");

        

        flipPlayer(inputHorizontal);

        //player attack bool followed by a reset of that value.
        playerAttack();
        resetValues();

        animator.SetFloat("Speed", Mathf.Abs(playerRigidBody.velocity.x));
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

        if (Input.GetButtonDown("Jump") && isGrounded() && canJump)
        {
            audioSource.PlayOneShot(playerJumpSound);
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

    private void playerAttack()
    {
        if (attack && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
            playerRigidBody.velocity = Vector2.zero;
        }
    }

    private void attackInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            attack = true;
        }
    }

    private bool canMove(Vector2 direction, float distance)
    {
        //casts a ray in the direction we are moving tot see if we can dash/roll there
        return Physics2D.Raycast(transform.position, direction, distance).collider == null;
    }

    public bool isGrounded()
    {
        float extraHeight = 0.1f;
        //Draws a line slightly longer than our box collider to detect if we are grounded
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerBoxCollider2D.bounds.center, playerBoxCollider2D.bounds.size, 0, Vector2.down, extraHeight, groundLayer);
        Color rayColor = Color.red;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            Debug.DrawRay(playerBoxCollider2D.bounds.center + new Vector3(playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (playerBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(playerBoxCollider2D.bounds.center - new Vector3(playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (playerBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(playerBoxCollider2D.bounds.center - new Vector3(playerBoxCollider2D.bounds.extents.x, playerBoxCollider2D.bounds.extents.x, 0), Vector2.right * (playerBoxCollider2D.bounds.extents.x), rayColor);
            return true;
        }
        else
        {
            Debug.DrawRay(playerBoxCollider2D.bounds.center + new Vector3(playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (playerBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(playerBoxCollider2D.bounds.center - new Vector3(playerBoxCollider2D.bounds.extents.x, 0), Vector2.down * (playerBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(playerBoxCollider2D.bounds.center - new Vector3(playerBoxCollider2D.bounds.extents.x, playerBoxCollider2D.bounds.extents.x, 0), Vector2.right * (playerBoxCollider2D.bounds.extents.x), rayColor);
            return false;
        }
    }

    //reset bools
    private void resetValues()
    {
        attack = false;
    }

    //getters

    public float getSpeed()
    {
        return movementSpeed;
    }

    //setters

    public void setJumping(bool j)
    {
        canJump = j;
    }

}
