using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkLizard : StateMachineBehaviour
{
    public float speed = 3f;
    public float attackRange = 3f;

    Transform player;
    Rigidbody2D rb;
    LizardMain lizard;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        lizard = animator.GetComponent<LizardMain>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        //lizard.lookAtPlayer();

        //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Lizard_Attack"))
        //{
        //    Vector2 targetPosition = new Vector2(lizard.target.position.x, lizard.transform.position.y);
        //    lizard.transform.position = Vector2.MoveTowards(lizard.transform.position, targetPosition, lizard.moveSpeed * Time.deltaTime);
        //}

        //if (!lizard.withinLimits() && !lizard.inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Lizard_Attack"))
        //{
        //    lizard.selectTarget();
        //}



        //if (Vector2.Distance(player.position, rb.position) <= attackRange)
        //{
        //    animator.SetTrigger("Attack");
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("Attack");
    }



}

