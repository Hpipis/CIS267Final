using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkPikeman : StateMachineBehaviour
{
    public float speed = 3f;
    public float attackRange = 3f;


    Transform player;
    Rigidbody2D rb;
    PikemanMain pikeman;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        pikeman = animator.GetComponent<PikemanMain>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        pikeman.lookAtPlayer();

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pike-man_Attack"))
        {
            Vector2 targetPosition = new Vector2(pikeman.target.position.x, pikeman.transform.position.y);
            pikeman.transform.position = Vector2.MoveTowards(pikeman.transform.position, targetPosition, pikeman.moveSpeed * Time.deltaTime);
        }

        if (!pikeman.withinLimits() && !pikeman.inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Pike-man_Attack"))
        {
            pikeman.selectTarget();
        }

        

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("Attack");
    }

    


}

