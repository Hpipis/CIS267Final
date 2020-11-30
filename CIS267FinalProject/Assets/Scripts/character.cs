using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private AudioSource audioSource;
    public Animator animator;
    public float movementSpeed = 5f;
    private float inputHorizontal;
    private Rigidbody2D playerRigidBody;
    public bool attack;
    public AudioClip playerSwordSwingSound;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        attackInput();
        playerAttackMovement();


    }

    public void playerAttackMovement()
    {
        playerAttackAction();
        resetValues();
    }

    public void playerAttackAction()
    {
        if (attack && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
        }
    }

    private void attackInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attack = true;
        }
    }

    private void resetValues()
    {
        attack = false;
    }

    private void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(playerSwordSwingSound);
    }


}
