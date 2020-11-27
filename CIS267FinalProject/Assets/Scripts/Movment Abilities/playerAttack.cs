using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Animator animator;
    public float movementSpeed = 5f;
    private float inputHorizontal;
    private Rigidbody2D playerRigidBody;
    private bool attack;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
       
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
}
