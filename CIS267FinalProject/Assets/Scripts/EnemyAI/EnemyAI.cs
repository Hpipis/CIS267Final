using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip swordSwingSound;

    //attack variables
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public float maxHealth = 1f;
    private float currentHealth;
    public Transform leftEdge;
    public Transform rightEdge;
    

    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    
    #endregion



    void Awake()
    {
        //Debug.Log("IsAwake");
        selectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }


    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!withinLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Pike-man_Attack"))
        {
            selectTarget();
        }

        if (inRange)
        {
            Debug.Log("IsRange");
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }



        if (hit.collider != null)
        {
            enemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {
            
            stopAttack();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            inRange = true;
            Flip();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            currentHealth--;
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void enemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            stopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            enemyMelee();
        }
        if (cooling)
        {
            animator.SetBool("attack", false);
        }
    }

    void Move()
    {
        Debug.Log("IsMoving");
        animator.SetBool("canWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pike-man_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }



    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //enemy fight ai


    void enemyMelee()
    {
        Debug.Log("attacking");

        
        timer = intTimer;
        attackMode = true;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);

    }

    

    void Die()
    {
        //Debug.Log("Enemy Died");
        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // misc scripting

    void cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(swordSwingSound);
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooldown();
        cooling = true;
    }

    private bool withinLimits()
    {
        return transform.position.x > leftEdge.position.x && transform.position.x < rightEdge.position.x;
    }
    
    private void selectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftEdge.position);
        float distanceToRight = Vector2.Distance(transform.position, rightEdge.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftEdge;
        }

        else
        {
            target = rightEdge;
        }

        Flip();

    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }

        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }


    //for player

    



}








