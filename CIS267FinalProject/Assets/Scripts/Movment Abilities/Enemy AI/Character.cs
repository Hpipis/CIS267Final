using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    
    public bool facingRight;

    public Animator Animator { get; private set; }

    void Start()
    {
        facingRight = true;
    }


    void Update()
    {
        changeDirection();
    }


    protected void changeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
