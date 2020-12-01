using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleState : IEnemyState
{

    private Enemy enemy;
    private float attackTimer;
    private float attackCooldown = 5;
    private bool canAttack;
    public bool attacking;


    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exectue()
    {

        attack();

        if (enemy.Target != null)
        {
            enemy.Move();
        }

        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void attack()
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
            enemy.animator.SetTrigger("attack");
            
        }
            
    }
}
