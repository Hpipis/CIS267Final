using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;
    private bool hasDoubleJump;

    public Animator animator;

    public AudioClip playerJumpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        pm = GetComponent<playerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !pm.isGrounded() && hasDoubleJump == true)
        {
            audioSource.PlayOneShot(playerJumpSound);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsJumping", true);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, pm.getJumpVelocity());
            hasDoubleJump = false;
        }
        if (pm.isGrounded() == true)
        {
            hasDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && pm.isGrounded() && hasDoubleJump == true)
        {
            Debug.Log("jump");
            audioSource.PlayOneShot(playerJumpSound);

        }
    }
}
