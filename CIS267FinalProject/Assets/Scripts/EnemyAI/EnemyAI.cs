using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D enemyRigidBody;

    public float agroRange = 5f;
    private float idleTimer;
    public float movementSpeed;
    private float idleDuration = 5;
    private float patrolTimer;
    private float patrolDuration = 5;
    private float attackTimer;
    private float attackCooldown = 5;
    public bool canAttack;
    public bool attacking;

    public bool idleState = true;
    public bool patrolState = false;
    public bool attack;
    public bool facingRight = true;
    public bool Target;
    public bool inMeleeRange;
           
    private float meleeRange;
    public Transform leftEdge;
    public Transform rightEdge;
    public Transform player;

    void Start()
    {

        enemyRigidBody = GetComponent<Rigidbody2D>();
        idleState = true;
        patrolState = false;
        canAttack = false;

        movementSpeed = 2f;

    }
    

    
    void Update()
    {

        
        GetDirection();
        lookAtTarget();
        playerAgro();
                

        if (patrolState == false && idleState == true && inMeleeRange == false)
        {
            enemyIdle();
        }

        if (patrolState == true && idleState == false && inMeleeRange == false)
        {
            Move();
            enemyPatrol();
        }

        if (patrolState == false && idleState == false && inMeleeRange == true)
        {
            enemyMelee();
        }


    }

    private void playerAgro()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //Debug.Log("Distance to player: " + distToPlayer);

        if (distToPlayer < agroRange && distToPlayer > 2)
        {
            Target = true;
            movementSpeed = 3;
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }

        //if (distToPlayer <= 2)
        //{
        //    movementSpeed = 0;
        //    Target = true;
        //    inMeleeRange = true;
        //    transform.Translate(GetDirection() * (movementSpeed));
        //    enemyRigidBody.velocity = Vector2.zero;
        //}

        else
        {
            Target = false;
            inMeleeRange = false;
           
        }

    }
   
    public void Move()
    {
        //need functionality to stop moving here.

         if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
         {
           
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
         }

        else
        {
            changeDirection();
        }

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void changeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private void lookAtTarget()
    {
        if (Target == true)
        {
            float xDirection = player.transform.position.x - transform.position.x;

            if (xDirection < 0 && facingRight || xDirection > 0 && !facingRight)
            {
                changeDirection();
            }
        }
    }

 

    private void enemyIdle()
    {
        Debug.Log("Im Idling");


        animator.SetBool("isIdle", true);
        animator.SetBool("isPatroling", false);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            patrolState = true;
            idleState = false;
            idleTimer = 0;
        }

    }

    private void enemyPatrol()
    {
        Debug.Log("Patroling");
        

        animator.SetBool("isIdle", false);
        animator.SetBool("isPatroling", true);

        

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            patrolState = false;
            idleState = true;
            patrolTimer = 0;
        }


    }

    private void enemyMelee()
    {
        Debug.Log("attacking");

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackTimer = 0;

        }

        if (canAttack)
        {
            canAttack = false;
            animator.SetTrigger("attack");

        }

    }

}








