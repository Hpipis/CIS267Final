using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI1 : MonoBehaviour
{
    
    //attack variables
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public AudioClip playerSwordSwingSound;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator animator;
    private AudioSource audioSource;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion



    void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
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
            animator.SetBool("canWalk", false);
            stopAttack();
        }









    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void enemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
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

    public void Move()
    {
        animator.SetBool("canWalk", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pike-man_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        

    }

   

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //enemy state ai
   

    private void enemyMelee()
    {
        Debug.Log("attacking");

        //attackTimer += Time.deltaTime;
        timer = intTimer;
        attackMode = true;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);


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
    private void stopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    private void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(playerSwordSwingSound);
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooldown();
        cooling = true;
    }

}