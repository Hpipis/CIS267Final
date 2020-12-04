using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public float maxHealth = 1f;
    private float currentPlayerHealth;
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
        currentPlayerHealth = maxHealth;
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


    //enemy weapons are on a trigger collision so that they don't push players off the map
    private void OnTriggerEnter2D(Collider2D WeaponHitMe)
    {
        if (WeaponHitMe.gameObject.CompareTag("EnemyWeapon"))
        {
            if (currentPlayerHealth <= 0)
            {
                Scene cur = SceneManager.GetActiveScene();
                SceneManager.LoadScene(cur.name);
            }

        }
        //if (WeaponHitMe.gameObject.CompareTag("PikeManHitBox"))
        //{
        //    float EnemyHealth = WeaponHitMe.gameObject.GetComponent<EnemyPikemanAI>().getHealth();

        //    if (EnemyHealth <= 0)
        //    {
        //        WeaponHitMe.gameObject.GetComponent<EnemyPikemanAI>().Die();
        //    }
        //    else if (EnemyHealth > 0)
        //    {
        //        Debug.Log("You hit enemy Pikeman");
        //    }
        //}
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////




}


