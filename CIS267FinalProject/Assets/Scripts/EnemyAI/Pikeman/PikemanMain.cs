using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanMain : MonoBehaviour
{
    public Animator animator;

    public Transform player;
    public Transform target;
    public Transform leftEdge;
    public Transform rightEdge;

    public bool isFlipped = true;
    public bool inRange;
    public int maxHealth = 1;
    public float moveSpeed = 3f;
    int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
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

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }

    public bool withinLimits()
    {
        return transform.position.x > leftEdge.position.x && transform.position.x < rightEdge.position.x;
    }

    public void selectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftEdge.position);
        float distanceToRight = Vector2.Distance(transform.position, rightEdge.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftEdge;
        }

        else
        {
            target = rightEdge;
        }

        Flip();

    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }

        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            inRange = true;
            Flip();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            inRange = true;
            Flip();
        }
    }

}
