using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundFX : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;
    //sound variables
    public AudioClip playerJumpSound;
    private AudioSource audioSource;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {

    }

    private void playJumpSOund()
    {
        if (Input.GetButtonDown("Jump") && pm.isGrounded())
        {
            audioSource.PlayOneShot(playerJumpSound);
        }
    }
}
