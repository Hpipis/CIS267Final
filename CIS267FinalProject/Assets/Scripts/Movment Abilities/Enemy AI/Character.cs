using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    
    public bool facingRight = true;

    public Animator Animator { get; private set; }

    public virtual void Start()
    {
        facingRight = true;
    }


    void Update()
    {
        changeDirection();
    }


    public void changeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
