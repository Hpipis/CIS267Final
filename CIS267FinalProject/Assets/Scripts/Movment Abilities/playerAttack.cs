using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public float maxHealth = 1f;
    private float currentHealth;
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
        currentHealth = maxHealth;
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
        if (attack && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            animator.SetTrigger("attack");
        }
    }

    private void attackInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////vv
    //damage scripting

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth--;
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("Enemy Died");
        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////




}


