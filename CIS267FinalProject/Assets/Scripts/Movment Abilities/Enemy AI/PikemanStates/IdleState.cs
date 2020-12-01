using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{

    private Enemy enemy;

    private float idleTimer;

    private float idleDuration = 5;
    
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exectue()
    {
        Debug.Log("Im Idling");
        Idle();

        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
            
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        //enemy.Animator.SetBool("isIdle", true);
        //enemy.Animator.SetBool("isPatroling", false);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
