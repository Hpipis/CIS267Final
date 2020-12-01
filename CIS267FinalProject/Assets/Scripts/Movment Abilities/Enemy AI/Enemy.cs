using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private IEnemyState currentState;
    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    public bool inMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;

            }

            return false;
        }

    }

    public float movementSpeed = 3f;

    public override void Start()
    {
        base.Start();

        ChangeState(new IdleState());

    }

        
    void Update()
    {
        currentState.Exectue();
        lookAtTarget();
    }

    private void lookAtTarget()
    {
        if (Target != null)
        {
            float xDirection = Target.transform.position.x - transform.position.x;

            if (xDirection < 0 && facingRight || xDirection > 0 && !facingRight)
            {
                changeDirection();
            }
        }
    }


    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        //need functionality to stop moving here.

        if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isPatroling", true);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
       
        else if (currentState is PatrolState)
        {
            changeDirection();
        }
        
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }
}
