using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundFX : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;
    private playerAttack pa;
    //sound variables
    public AudioClip playerJumpSound;
    public AudioClip playerSwordSwingSound;
    private AudioSource audioSource;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        playJumpSound();
        playSwordSwingSound();
    }

    private void playJumpSound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            audioSource.PlayOneShot(playerJumpSound);

        }
    }

    private void playSwordSwingSound()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("sword");
            audioSource.PlayOneShot(playerSwordSwingSound);
        }
    }
}
