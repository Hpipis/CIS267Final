using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 1;
    int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("DEAD");
        animator.SetBool("isDead", true);
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.376768f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }
}
