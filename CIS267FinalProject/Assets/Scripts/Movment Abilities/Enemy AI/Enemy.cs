using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private IEnemyState currentState;
    public float movementSpeed = 5f;

    void Start()
    {
        ChangeState(new IdleState());

    }

    
    void Update()
    {
        currentState.Exectue();
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
        Animator.SetFloat("speed", 1);

        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));


    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }
}
