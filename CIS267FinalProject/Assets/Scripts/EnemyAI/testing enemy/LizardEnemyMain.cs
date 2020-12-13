using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardEnemyMain : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip lizardChargingSound;
    public AudioClip lizardAttackSound;
    public AudioClip lizardBlastSound;
    public AudioClip klizardDeathSound;
    public AudioClip walking;

    public Transform rayCast;
    public Transform player;
    public Transform leftEdge;
    public Transform rightEdge;
    private Transform target;

    public LayerMask raycastMask;

    public float attackDistance;
    public bool isFlipped = true;
    public bool inRange;
    public float rayCastLength;
    public bool attackMode;
    public bool cooling;
    public float moveSpeed;
    public float timer;

    float distanceToPlayer;

    private RaycastHit2D hit;
    private float distance;
    private float intTimer;

    //used to make enemies not kill you before even attacking -  this links to Enemy HitBox script
    //private bool hasAttacked;
    //private float hasAttackedTimer = 0f;
    //private float hasAttackOffset = .6f;

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Lizard_Attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {

            StopAttack();
        }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);



        if (distance > attackDistance)
        {

            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            animator.SetBool("Attack", false);
        }
    }

    void Move()
    {
        animator.SetBool("canWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Lizard_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        if (!withinLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Lizard_Attack"))
        {
            SelectTarget();
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }


    public bool withinLimits()
    {
        return transform.position.x > leftEdge.position.x && transform.position.x < rightEdge.position.x;
    }

    public void SelectTarget()
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

    public void TriggerCooling()
    {
        cooling = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
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

    private bool InsideofLimits()
    {
        return transform.position.x > leftEdge.position.x && transform.position.x < rightEdge.position.x;
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





    private void LizardCharging()
    {
        audioSource.PlayOneShot(lizardChargingSound);
    }

    private void LizardAttack()
    {
        audioSource.PlayOneShot(lizardAttackSound);
    }

    private void LizardBlast()
    {
        audioSource.PlayOneShot(lizardBlastSound);
    }
    private void LizardDeath()
    {
        audioSource.PlayOneShot(klizardDeathSound);
    }

    private void WalkingLizard()
    {
        if (distanceToPlayer < 30 && distanceToPlayer >= 20)
        {
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(walking);
        }
        if (distanceToPlayer < 20 && distanceToPlayer >= 14)
        {
            audioSource.volume = 0.25f;
            audioSource.PlayOneShot(walking);
        }

        if (distanceToPlayer < 14)
        {
            audioSource.volume = .5f;
            audioSource.PlayOneShot(walking);
        }
    }


    //public bool getAttacking()
    //{
    //    return hasAttacked;
    //}

}
